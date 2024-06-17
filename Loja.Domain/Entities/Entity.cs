using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Loja.Domain.Entities
{
    public abstract class Entity
    {
        /// <summary>
        /// Identificador único da entidade.
        /// </summary>
        [JsonInclude]
        public int Id { get; set; }

        /// <summary>
        /// Data de exclusão do registro.
        /// </summary>
        public DateTimeOffset? DeletedAt { get; private set; }

        public void SoftDelete()
        {
            DeletedAt = DateTime.UtcNow;
        }
    }
}
