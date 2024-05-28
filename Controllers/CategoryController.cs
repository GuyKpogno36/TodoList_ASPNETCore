using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using Todo_List_ASPNETCore.DAL;
using Todo_List_ASPNETCore.Models;

namespace Todo_List_ASPNETCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly TodoListContext contexteEF;

        [HttpGet]
        public IActionResult GetCategories()
        {
            var categories = contexteEF.CATEGORY.ToList();
            return Ok(categories);
        }

        [HttpPost]
        public IActionResult CreateCategory([FromBody] CategoryModel category)
        {
            contexteEF.CATEGORY.Add(new CATEGORY { Category_Name = category.Name });
            contexteEF.SaveChanges();
            return CreatedAtAction(nameof(GetCategoryById), new { id = category.Id }, category);
        }

        [HttpGet("{id}")]
        public IActionResult GetCategoryById(int id)
        {
            var category = contexteEF.CATEGORY.FirstOrDefault(c => c.Category_ID == id);
            if (category == null) return NotFound();
            
            return Ok(category);
        }
    }
}
