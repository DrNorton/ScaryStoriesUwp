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
