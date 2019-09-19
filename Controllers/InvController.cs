using System.Collections.Generic;
using System.Linq;
using inventoryapi;
using inventoryApi.Models;
using Microsoft.AspNetCore.Mvc;


namespace inventoryAPI.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class InvController : ControllerBase
  {
    private DatabaseContext context;

    public InvController(DatabaseContext _context) => this.context = _context;

    [HttpPost]
    public ActionResult<Item> CreateItem([FromBody]Item entry)
    {
      context.Items.Add(entry);
      context.SaveChanges();
      return entry;
    }
    [HttpGet]
    public ActionResult<IEnumerable<Item>> GetAllItems()
    {
      var item = context.Items.OrderByDescending(i => i.DateOrdered);
      return item.ToList();
    }

    [HttpGet("{id}")]
    public ActionResult GetOneItem(int id)
    {

      var item = context.Items.FirstOrDefault(i => i.Id == id);

      if (item == null)
      {
        return NotFound();
      }
      else
      {
        return Ok(item);
      }
    }

    [HttpPut("{id}")]

    public ActionResult<Item> GetUpdate([FromBody]Item entry, int id)
    {
      var itemToUpdate = context.Items.FirstOrDefault(i => i.Id == id);
      context.Items.Update(itemToUpdate);
      context.SaveChanges();
      return entry;
    }

    [HttpDelete("{id}")]
    public ActionResult<Item> GetDelete(int id)
    {
      var itemToDelete = context.Items.FirstOrDefault(i => i.Id == id);
      context.Items.Update(itemToDelete);
      context.SaveChanges();
      return itemToDelete;
    }

    [HttpGet("IOS")]
    public ActionResult GetOuttem()
    {
      var item = context.Items.FirstOrDefault(i => i.NumberInStock == 0);
      if (item == null)
      {
        return NotFound();
      }
      else
      {
        return Ok(item);
      }
    }

    [HttpGet("{SKU}")]
    public ActionResult GetItemSKU(int SKU)
    {
      var item = context.Items.FirstOrDefault(i => i.SKU == SKU);
      if (item == null)
      {
        return NotFound();
      }
      else
      {
        return Ok(item);
      }
    }

  }
}




//  Create a GET endpoint to get all items that are out of stock

//  Create a GET endpoint that allows the to search for an item based on SKU