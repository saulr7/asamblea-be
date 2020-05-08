
using Microsoft.EntityFrameworkCore;

namespace Asamblea_BE.Entities
{
    public class ApplicacionContext : DbContext
    {
        public ApplicacionContext(DbContextOptions<ApplicacionContext> options) : base(options)
        {

        }

        public DbSet< Usuario> Usuario { get; set; }
        public DbSet<Archivo> Archivo { get; set; }
        public DbSet<VariableDeConfiguracion> VariableDeConfiguracion { get; set; }
        public DbSet<RepositorioArchivo> RepositorioArchivo { get; set; }
        public DbSet<Evaluacion> Evaluacion { get; set; }
        public DbSet<EvaluacionesCompletadas> EvaluacionesCompletadas { get; set; }
        public DbSet<Preguntas> Pregunta { get; set; }
        public DbSet<OpcionesDeRespuestas> OpcionesDeRespuestas { get; set; }
        public DbSet<PreguntaOpcionesDeRespuesta> PreguntaOpcionesDeRespuestas { get; set; }
        public DbSet<Respuesta> Respuesta { get; set; }
        public DbSet<Chat> Chat { get; set; }
        public DbSet<Mensajes> Mensajes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder dbContextOptionsBuilder)
        {

        }
    }
}
