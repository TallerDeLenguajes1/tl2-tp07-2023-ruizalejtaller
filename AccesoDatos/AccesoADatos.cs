using System.Text.Json;
namespace espTareas;

public class AccesoADatos
{
    public List<Tareas> Obtener()
    {
        string file = "Tareas.json";
        var LTareas = new List<Tareas>();
            if(File.Exists(file))
            {
                using StreamReader archivo = new(file);

                string objson = archivo.ReadToEnd();
                LTareas = JsonSerializer.Deserialize<List<Tareas>>(objson);

                archivo.Close();
            }
        return LTareas;
    }

    public void Guardar(List<Tareas> Tareas)
    {
        string file = "Tareas.json";
        string jsonString = JsonSerializer.Serialize(Tareas);

            using StreamWriter archivo = new(file);
            archivo.WriteLine(jsonString);
            archivo.Close();

    }
}