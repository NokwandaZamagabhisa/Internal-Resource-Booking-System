﻿using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ResourceBooking.Core.Entities;
using ResourceBooking.Core.Gateways;
using ResourceBooking.Web.Models;

namespace ResourceBooking.Web.Controllers
{
    public class BookingsController : Controller
    {
        private readonly IBookingGateway _bookingGateway;
        private readonly IResourceGateway _resourceGateway;

        public BookingsController(IBookingGateway bookingGateway, IResourceGateway resourceGateway)
        {
            _bookingGateway = bookingGateway;
            _resourceGateway = resourceGateway;
        }

        // GET: Bookings
        public async Task<IActionResult> Index()
        {
            var bookings = await _bookingGateway.GetAll();
            var dtoList = bookings.Select(MapBookingToDto).ToList();
            return View(dtoList);
        }

        private static BookingDto MapBookingToDto(Booking booking)
        {
            return new BookingDto
            {
                Id = booking.Id,
                ResourceId = booking.ResourceId,
                StartTime = booking.StartTime,
                EndTime = booking.EndTime,
                BookedBy = booking.BookedBy,
                Purpose = booking.Purpose,
                Resource = MapResourceToDto(booking),
            };
        }

        private static ResourceDto MapResourceToDto(Booking b)
        {
            return new ResourceDto
            {
                Id = b.Resource.Id,
                Name = b.Resource.Name,
                Description = b.Resource.Description,
                Location = b.Resource.Location,
                Capacity = b.Resource.Capacity,
                IsAvailable = b.Resource.IsAvailable
            };
        }

        // GET: Bookings/Create
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var resources = await _resourceGateway.GetAllAvailable();
            ViewData["ResourceId"] = new SelectList(resources, "Id", "Description");
            return View();
        }

        

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var bookings = await _bookingGateway.GetAll();
            return Ok(bookings);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int resourceId)
        {
            var bookings = await _bookingGateway.GetUpcomingBookingsForResource(resourceId);
            var dtoList = bookings.Select(MapBookingToDto).ToList();
            ViewData["ResourceId"] = resourceId; // Pass ResourceId for view context
            return Ok(dtoList);
        }

        [HttpPost]
        public async Task<IActionResult> Create(BookingDto dto)
        {
            var validationResult = BookingDto.ValidateBooking(dto, new ValidationContext(dto));
            if (validationResult != null)
            {
                ModelState.AddModelError("", validationResult.ErrorMessage);
                //Can create method for this repeated code
                var resources = await _resourceGateway.GetAll();
                ViewData["ResourceId"] = new SelectList(resources, "Id", "Description", dto.ResourceId);
                return View(dto);
            }

            if (!ModelState.IsValid)
            {
                //Can create method for this repeated code
                var resourcesForError = await _resourceGateway.GetAll();
                ViewData["ResourceId"] = new SelectList(resourcesForError, "Id", "Description", dto.ResourceId);
                return View(dto);

            }

            var conflictingBookings = await _bookingGateway.GetConflictingBookings(dto.ResourceId, dto.StartTime, dto.EndTime);
            if (conflictingBookings.Any())
            {
                ModelState.AddModelError("", "This resource is already booked during the requested time. Please choose another slot or resource, or adjust your times.");

                //Can create method for this repeated code
                var resources = await _resourceGateway.GetAll();
                ViewData["ResourceId"] = new SelectList(resources, "Id", "Description", dto.ResourceId);
                return View(dto);

            }

            var booking = new Booking
            {
                ResourceId = dto.ResourceId,
                StartTime = dto.StartTime,
                EndTime = dto.EndTime,
                BookedBy = dto.BookedBy,
                Purpose = dto.Purpose
            };

            await _bookingGateway.Add(booking);
            return RedirectToAction(nameof(Index));
        }



    }


}
