using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class TrainingProgram
    {
        public int TrainingProgramId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int TrainingDurationInMinutes { get; set; }
        public string TrainingType { get; set; }
        [JsonIgnore]
        public List<Session> Sessions { get; set; }

    }
}
