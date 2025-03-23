using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RealEstateAPI.Models;

namespace RealEstateAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PropertyController : ControllerBase
    {
        private static readonly List<Property> properties = new()
        {
            new Property { Id = 1, Address = "123 Main St", Type = "House", Price = 500000 },
            new Property { Id = 2, Address = "456 Elm St", Type = "Apartment", Price = 300000 }
        };

        [HttpGet]
        public IActionResult GetProperties() => Ok(properties);

        [HttpGet("{id}")]
        public IActionResult GetProperty(int id)
        {
            var property = properties.FirstOrDefault(p => p.Id == id);
            return property != null ? Ok(property) : NotFound("Property not found");
        }

        [Authorize]
        [HttpPost]
        public IActionResult AddProperty(Property property)
        {
            property.Id = properties.Count + 1;
            properties.Add(property);
            return CreatedAtAction(nameof(GetProperty), new { id = property.Id }, property);
        }

        [Authorize]
        [HttpPut("{id}")]
        public IActionResult UpdateProperty(int id, Property updatedProperty)
        {
            var property = properties.FirstOrDefault(p => p.Id == id);
            if (property == null)
                return NotFound("Property not found");

            property.Address = updatedProperty.Address;
            property.Type = updatedProperty.Type;
            property.Price = updatedProperty.Price;
            return Ok(property);
        }

        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult DeleteProperty(int id)
        {
            var property = properties.FirstOrDefault(p => p.Id == id);
            if (property == null)
                return NotFound("Property not found");

            properties.Remove(property);
            return NoContent();
        }
    }
}
