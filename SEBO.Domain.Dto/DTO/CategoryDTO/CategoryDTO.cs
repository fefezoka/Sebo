﻿using SEBO.Domain.Entities.ProductAggregate;

namespace SEBO.Domain.Dto.DTO.CategoryDTO
{
    public class CategoryDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public CategoryDTO(Category category)
        {
            Id = category.Id;
            Name = category.Name;
            Description = category.Description;
        }
    }
}
