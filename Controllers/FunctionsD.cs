
using Microsoft.AspNetCore.Mvc;
using Data;
using Classes;


namespace Classes;


[ApiController]
[Route("[Controller]")]

public class DegreeController : ControllerBase
{

    private readonly DataContext _context;

    public DegreeController(DataContext dataContext)
    {
        _context = dataContext;
    }

    /// <summary>
    /// Mostrar todos los grados
    /// </summary>
    /// <returns>Todos los grados</returns>
    /// <response code="200">Devuelve el listado de grados</response>
    /// <response code="500">Si hay algún error</response>
    [HttpGet]
    public ActionResult<List<Degree>> Get()
    {
        List<Degree> degree = _context.Degrees.ToList();


        return degree == null ? NotFound()
              : Ok(degree);
    }
    /// <summary>
    /// Buscar por id
    /// </summary>
    /// <returns>Todos los grados con el mismo id</returns>
    /// <response code="200">Devuelve el listado de grados con este id</response>
    /// <response code="500">Si hay algún error</response>
    [HttpGet]
    [Route("{id:int}")]
    public ActionResult<Degree> Get(int id)
    {
        Degree Degree = _context.Degrees.Find(id);
        return Degree == null ? NotFound()
            : Ok(Degree);
    }
    /// <summary>
    /// Buscar por nombre
    /// </summary>
    /// <returns>Todos los grados con el mismo nombre</returns>
    /// <response code="200">Devuelve el listado de grados</response>
    /// <response code="500">Si hay algún error</response>
    //Buscar por nombre
    [HttpGet]
    [Route("name")]
    public ActionResult<Degree> Get(string name)
    {
        List<Degree> degree = _context.Degrees.Where(x => x.name.Contains(name)).OrderByDescending(x => x.name).ToList();
        //buscar por nombre   
        return degree == null ? NotFound()
            : Ok(degree);
    }
    /// <summary>
    /// añadir grados
    /// </summary>
    /// <returns>Todos los grados</returns>
    /// <response code="201">Se ha creado correctamente</response>
    /// <response code="500">Si hay algún error</response>
    [HttpPost]
    public ActionResult<User> Post([FromBody] Degree degree)
    {

        degree.id = 0;
        _context.Degrees.Add(degree);
        _context.SaveChanges();

        string resourceUrl = Request.Path.ToString() + "/" + degree.id;
        return Created(resourceUrl, degree);
    }
    /// <summary>
    ///Actualizar los grados
    /// </summary>
    /// <returns>Todos los grados</returns>
    /// <response code="201">Devuelve el listado de grados</response>
    /// <response code="500">Si hay algún error</response>
    [HttpPut("{id}")]
    public ActionResult<User> Update([FromBody] Degree degree, int id)
    {
        Degree degreeToUpdate = _context.Degrees.Find(id);
        if (degreeToUpdate == null)
        {
            return NotFound("grado no encontrado");
        }
        degreeToUpdate.name = degree.name;
        degreeToUpdate.nameDegree = degree.nameDegree;
        degreeToUpdate.quedanPlazas = degree.quedanPlazas;
        degreeToUpdate.dataExpediente = degree.dataExpediente;
        degreeToUpdate.price = degree.price;
        degreeToUpdate.cantidadPlazas = degree.cantidadPlazas;


        _context.SaveChanges();
        string resourceUrl = Request.Path.ToString() + "/" + degreeToUpdate.name;
        
        return Created(resourceUrl, degreeToUpdate);
    }

    /// <summary>
    /// Eliminar grados seleccionados
    /// </summary>
    /// <returns>Todos los grados</returns>
    /// <response code="200">Se ha eliminado</response>
    /// <response code="500">Si hay algún error</response>
    [HttpDelete("{id:int}")]
    public ActionResult Delete(int id)
    {
        Degree degreeToDelete = _context.Degrees.Find(id);
        if (degreeToDelete == null)
        {
            return NotFound("menu no encontrado");
        }
        _context.Degrees.Remove(degreeToDelete);
        _context.SaveChanges();
        if (degreeToDelete == null)
        {
            return NotFound();
        }
        return Ok(degreeToDelete);
    }

}
