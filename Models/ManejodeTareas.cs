namespace espTareas;

public class ManejoDeTareas
{
    private AccesoADatos Access;
    private List<Tareas> LTareas;

    public ManejoDeTareas(AccesoADatos Access)
    {
        this.Access = Access;
    }

    public void CargarTareas()
    {
        LTareas = Access.Obtener();
    }

    public bool AgregarTarea(Tareas Tar)
    {
        if (Tar != null)
        {
            LTareas.Add(Tar);
            Tar.Id = LTareas.Max(t => t.Id)+1;
            Access.Guardar(LTareas);
            return true;
        }
        return false;
    }

    public Tareas BuscarTarea(int Id)
    {
        return LTareas.FirstOrDefault(t => t.Id == Id);
    }

    public bool ActualizarTarea (Tareas Tar)
    {
        var Tarea = BuscarTarea(Tar.Id);

        if (Tarea != null)
        {
            if(Tar.Titulo != "string" && Tar.Titulo != "")
            {
                Tarea.Titulo = Tar.Titulo;
            }

            if(Tar.Descripcion != "string" && Tar.Descripcion != "")
            {
                Tarea.Descripcion = Tar.Descripcion;
            }

            Tarea.Estado = Tar.Estado;

            Access.Guardar(LTareas);

            return true;
        }

        return false;
    }

    public bool EliminarTarea(int Id)
    {
        var Tar = BuscarTarea(Id);

        if (Tar != null)
        {
            LTareas.Remove(Tar);
            Access.Guardar(LTareas);

            return true;
        }

        return false;
    }

    public List<Tareas> MostrarTareas()
    {
        return LTareas;
    }

    public List<Tareas> MostrarTareasCompletadas()
    {
        return LTareas.FindAll(t => t.Estado == Estados.Completada);
    }
}