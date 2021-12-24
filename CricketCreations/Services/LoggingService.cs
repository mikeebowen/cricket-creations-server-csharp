using System;
using CricketCreations.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RockLib.Logging;

namespace CricketCreations.Services
{
    public class LoggingService : ILoggerService
    {
        private readonly ILogger _logger;

        public LoggingService(ILogger logger)
        {
            _logger = logger;
        }

        public StatusCodeResult Error(Exception ex)
        {
            _logger.Error("Request failed.", ex);
            return new StatusCodeResult(StatusCodes.Status500InternalServerError);
        }

        public ObjectResult Info(DbUpdateException ex)
        {
            _logger.Info("Failed to update database.", ex);
            return new ObjectResult(new { Errors = new[] { new { Message = ex.InnerException?.Message != null ? ex.InnerException.Message : ex.Message } } }) { StatusCode = StatusCodes.Status500InternalServerError };
        }
    }
}
