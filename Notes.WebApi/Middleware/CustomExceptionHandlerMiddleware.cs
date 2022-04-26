﻿using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Notes.Application.Common.Exeptions;
using Microsoft.AspNetCore.Http;
using FluentValidation;

namespace Notes.WebApi.Middleware
{
    public class CustomExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        // выхывает делигат или перехватывает исключение
        public CustomExceptionHandlerMiddleware(RequestDelegate next)=>
            _next = next;   

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            var code = HttpStatusCode.InternalServerError;
            var result = string.Empty;
            switch (ex)
            {
                case ValidationException validationException:
                    code = HttpStatusCode.BadRequest;
                    result = JsonSerializer.Serialize(validationException);
                    break;
                case NotFoundExeption:
                     code = HttpStatusCode.NotFound;
                    break;
            }
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;    

            if (result == string.Empty)
            {
                result = JsonSerializer.Serialize(new { eror = ex.Message });
            }

            return context.Response.WriteAsync(result);
        }
    }
}
