﻿using System.Text.Json.Serialization;
using SEBO.Domain.Utility.Abstractions;

namespace SEBO.Domain.Dto.DTO.Base
{
    public class BaseResponseDTO<T> where T : class
    {
        public bool IsSuccess { get; private set; } = true;


        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public List<T>? Content { get; private set; } = null;

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public PagedList<T>? PagedContent { get; private set; } = null;

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public List<string>? Errors { get; private set; } = null;

        public BaseResponseDTO() { }
        public BaseResponseDTO(bool isSuccess)
        {
            IsSuccess = isSuccess;
        }

        public BaseResponseDTO<T> WithSuccesses(IEnumerable<T> content, bool isSuccess = true)
        {
            if (Content is null) Content = new List<T>();
            Content.AddRange(content);
            IsSuccess = isSuccess;
            return this;
        }
        public BaseResponseDTO<T> WithErrors(IEnumerable<string> errors, bool isSuccess = false)
        {
            if (Errors is null) Errors = new List<string>();
            Errors.AddRange(errors);
            IsSuccess = isSuccess;
            return this;
        }

        public BaseResponseDTO<T> AddContent(T content, bool isSuccess = true)
        {
            if (Content is null) Content = new List<T>();
            Content.Add(content);
            IsSuccess = isSuccess;
            return this;
        }

        public BaseResponseDTO<T> AddContent(IEnumerable<T> content, bool isSuccess = true)
        {
            if (Content is null) Content = new List<T>();
            Content.AddRange(content);
            IsSuccess = isSuccess;
            return this;
        }

        public BaseResponseDTO<T> AddContent(PagedList<T> content, bool isSuccess = true)
        {
            PagedContent ??= new PagedList<T>();
            PagedContent = content;
            IsSuccess = isSuccess;
            return this;
        }

        public BaseResponseDTO<T> AddError(string error, bool isSuccess = false)
        {
            if (Errors is null) Errors = new List<string>();
            Errors.Add(error);
            IsSuccess = isSuccess;
            return this;
        }
    }
}