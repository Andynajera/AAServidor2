using Microsoft.AspNetCore.Mvc;
using Data;
using Classes;


namespace Classes;


[ApiController]
[Route("[Controller]")]

public class PagosController : ControllerBase
{

    private readonly DataContext _context;

    public PagosController(DataContext dataContext)
    {
        _context = dataContext;
    }
    

    /// <summary>
    /// Mostrar todos los pagos
    /// </summary>
    /// <returns>Todos los pagos</returns>
    /// <response code="200">Devuelve el listado de pagos</response>
    /// <response code="500">Si hay algún error</response>
    
    [HttpGet]
    public ActionResult<List<Pago>> Get()
    {
        List<Pago> pago =_context.Pagos.OrderByDescending(x => x.pagado).ToList();
        //Revisar orden
       
        return   Ok(pago);
        
    }
   /* [HttpGet]
    [Route("{id}")]
    public ActionResult<Pago> Get(int id)
    {
        //buscar por nombre
        return pago == null? NotFound()
            : Ok(pago);
    }
*/
/// <summary>
    /// Mostrar  los pagos con este id
    /// </summary>
    /// <returns>Todos los pagos con este id</returns>
    /// <response code="200">Devuelve el listado de pagos con este id</response>
    /// <response code="500">Si hay algún error</response>
    [HttpGet]
    [Route("pagado")] 
    public ActionResult<Pago> Get(bool pagado)
    {
        
  List<Pago> pago =_context.Pagos.Where(x=> x.pagado == pagado).ToList();
        //buscar por bool   
        return pago == null? NotFound()
            : Ok(pago);
    }
    /// <summary>
    /// añadir pagos
    /// </summary>
    /// <returns>Todos los pagos</returns>
    /// <response code="201">Se ha creado correctamente</response>
    /// <response code="500">Si hay algún error</response>
   /* [HttpPost]
    public ActionResult<User> Post([FromBody] User user)
    {
        user.id=0;
        _context.User.Add(user);
        _context.SaveChanges();
        string resourceUrl = Request.Path.ToString() + "/" + user.id;
        return Created(resourceUrl, user);
    }
*/

    
    [HttpPost]
    public ActionResult<Pago> Post([FromBody] Pago pago )
    {
         Pago existingPagoItems= _context.Pagos.Find(pago.id);
         
        if (existingPagoItems != null)
        {
            return Conflict("Ya existe un elemento ");
        }
        _context.Pagos.Add(pago);
        _context.SaveChanges();

        string resourceUrl = Request.Path.ToString() + "/" + pago.id;
       
        return Created(resourceUrl, pago);
    }
    /// <summary>
    ///Actualizar los Pagos
    /// </summary>
    /// <returns>Todos los Pagos</returns>
    /// <response code="201">Devuelve el listado de pagos</response>
    /// <response code="500">Si hay algún error</response>
    [HttpPut("{id}")]
    public ActionResult<Pago> Update([FromBody] Pago pago, int id)
    {
        Pago pagoToUpdate = _context.Pagos.Find(id);
         
        if (pagoToUpdate == null)
        {
            return NotFound("usuario no encontrado");
        }
        pagoToUpdate.name=pago.name;
        pagoToUpdate.price=pago.price;
        pagoToUpdate.date=pago.date;
        pagoToUpdate.total=pago.total;
        pagoToUpdate.pagado=pago.pagado;
        pagoToUpdate.notes=pago.notes;

        _context.SaveChanges();
        string resourceUrl = Request.Path.ToString() + "/" + pagoToUpdate.notes;

        return Created(resourceUrl, pagoToUpdate);
    }

    /// <summary>
    /// Eliminar pagos seleccionados
    /// </summary>
    /// <returns>Todos los pagos</returns>
    /// <response code="200">Se ha eliminado</response>
    /// <response code="500">Si hay algún error</response>
   /*     [HttpDelete("{id:int}")]
    public ActionResult Delete(int id)
    {
        Pago pagoToDelete = _context.Pagos.Find(id);
        if (pagoToDelete == null)
        {
            return NotFound("menu no encontrado");
        }
        _context.Pagos.Remove(pagoToDelete);
        _context.SaveChanges();
        return Ok(pagoToDelete);
    }
    */
      [HttpDelete("{id:int}")]
    public ActionResult Delete(int id)
    {
        if (id == null)
        {
            return BadRequest();
        }
        else
        {
            Pago pagoToDelete = _context.Pagos.Find(id);
            if (pagoToDelete == null)
            {
                return NotFound("menu no encontrado");
            }
            _context.Pagos.Remove(pagoToDelete);
            _context.SaveChanges();
            var orders = _context.OrderPros.ToList();
            orders.ForEach(o =>
            {
                if (o.PagoId == id)
                {
                    _context.OrderPros.Remove(o);
                }
                _context.SaveChanges();
                
            });
            return Ok();

        }
    }

}
