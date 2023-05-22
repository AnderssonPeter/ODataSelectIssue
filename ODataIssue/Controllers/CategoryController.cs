using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace ODataIssue.Controllers;

public class CategoryController : ODataController
{
    private static List<Category> customers = new List<Category>(
        Enumerable.Range(1, 3).Select(idx => new Category
        {
            Id = idx,
            Name = $"Category {idx}",
            Description = $"Category description {idx}",
            Products = new List<Product>(
                Enumerable.Range(1, 2).Select(dx => new Product
                {
                    Id = (idx - 1) * 2 + dx,
                    Name = $"Product {dx}",
                    Description = $"Product description {idx}",
                }))
        }));

    [EnableQuery]
    public ActionResult<IEnumerable<Category>> Get()
    {
        return Ok(customers);
    }

    [EnableQuery]
    public ActionResult<Category> Get([FromRoute] int key)
    {
        var item = customers.SingleOrDefault(d => d.Id.Equals(key));

        if (item == null)
        {
            return NotFound();
        }

        return Ok(item);
    }
}


public class Category
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public ICollection<Product> Products { get; set; }
}

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
}