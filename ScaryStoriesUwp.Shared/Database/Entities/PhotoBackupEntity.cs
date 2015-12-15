using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace ScaryStoriesUwp.Shared.Database.Entities
{
    [Table("Photos")]
    public class PhotoBackupEntity
    {
        public string Id { get; set; }
        public byte[] Image { get; set; }
        public byte[] Thumb { get; set; }
    }
}
