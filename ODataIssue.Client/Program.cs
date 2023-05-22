// See https://aka.ms/new-console-template for more information
using Default;

Console.WriteLine("Hello, World!");
var container = new Container(new Uri("http://localhost:5184/odata"));
container.BuildingRequest += Container_BuildingRequest;

void Container_BuildingRequest(object? sender, Microsoft.OData.Client.BuildingRequestEventArgs e)
{
    Console.WriteLine(e.RequestUri);
}

var query = container.Categories.Select(c =>
new {
    c.Id, 
    c.Name,
    Products = c.Products.Select(p =>
        new
        {
            p.Name,
            p.Description
        }
    )
});
var list = query.ToList();