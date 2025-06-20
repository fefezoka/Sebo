﻿using System.Net;
using Newtonsoft.Json;
using SEBO.Domain.Utility.Exceptions;
using SEBO.Domain.Dto.DTO.Base;
using Microsoft.AspNetCore.Http;

namespace SEBO.Middleware
{
    public class RequestMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                ConfigureHeaders(context);
                await next(context);
            }
            catch (NotFoundException ex)
            {
                await HandleNotFoundExceptionAsync(context, ex);
            }
            catch (BadRequestException ex)
            {
                await HandleBadRequestExceptionAsync(context, ex);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private void ConfigureHeaders(HttpContext context)
        {
            context.Response.Headers.Add("Strict-Transport-Security", "max-age=31536000; includeSubDomains");
            context.Response.Headers.Add("Content-Security-Policy", "default-src 'self'");
            context.Response.Headers.Add("X-Frame-Options", "SAMEORIGIN");
            context.Response.Headers.Add("X-Content-Type-Options", "nosniff");
            context.Response.Headers.Add("Referrer-Policy", "strict-origin-when-cross-origin");
            context.Response.Headers.Add("Permissions-Policy", "accelerometer=(),autoplay=(),camera=(),display-capture=(),document-domain=(),encrypted-media=(),fullscreen=(),geolocation=(),gyroscope=(),magnetometer=(),microphone=(),midi=(),payment=(),picture-in-picture=(),publickey-credentials-get=(),screen-wake-lock=(),sync-xhr=(self),usb=(),web-share=(),xr-spatial-tracking=()");
        }

        private async Task HandleNotFoundExceptionAsync(HttpContext context, NotFoundException exception)
        {
            var contentType = "application/json";
            var statusCode = (int)HttpStatusCode.NotFound;

            var baseResponse = new BaseResponseDTO<string>().WithErrors(exception.Errors);
            var response = JsonConvert.SerializeObject(baseResponse);

            context.Response.ContentType = contentType;
            context.Response.StatusCode = statusCode;
            await context.Response.WriteAsync(response);
        }

        private async Task HandleBadRequestExceptionAsync(HttpContext context, BadRequestException exception)
        {
            var contentType = "application/json";
            var statusCode = (int)HttpStatusCode.BadRequest;

            var baseResponse = new BaseResponseDTO<string>().WithErrors(exception.Errors);
            var response = JsonConvert.SerializeObject(baseResponse);

            context.Response.ContentType = contentType;
            context.Response.StatusCode = statusCode;
            await context.Response.WriteAsync(response);
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var contentType = "application/json";
            var statusCode = (int)HttpStatusCode.InternalServerError;

            var baseResponse = new BaseResponseDTO<string>().AddError(exception.Message);
            var response = JsonConvert.SerializeObject(baseResponse);

            context.Response.ContentType = contentType;
            context.Response.StatusCode = statusCode;
            await context.Response.WriteAsync(response);
        }
    }
}