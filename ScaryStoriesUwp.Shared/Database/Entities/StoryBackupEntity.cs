using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace ScaryStoriesUwp.Shared.Database.Entities
{
    [Table("Stories")]
    public class StoryBackupEntity
    {
        public string Id { get; set; }

        public string CategoryId { get; set; }

        public string SourceId { get; set; }

        public string PhotoId { get; set; }

        public string Name { get; set; }
        public long Likes { get; set; }
        public string Text { get; set; }
        public byte[] Thumb { get; set; }

        public string Url { get; set; }
    }
}
