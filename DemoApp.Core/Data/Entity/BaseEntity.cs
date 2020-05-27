using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoApp.Core.Data.Enity
{
    public abstract class BaseEntity
    {
        [Key]
        [Column("Id")]
        public string Id { get; set; }

        [Column("Create_By")]
        public string CreateBy { get; set; }

        [Column("Create_At")]
        public DateTime? CreateAt { get; set; }

        [Column("Update_By")]
        public string UpdateBy { get; set; }

        [Column("Update_At")]
        public DateTime? UpdateAt { get; set; }

        public virtual void SetDefaultInsertValue(string username)
        {
            Id = Guid.NewGuid().ToString();
            CreateAt = DateTime.UtcNow;
            CreateBy = username;
            UpdateAt = DateTime.UtcNow;
            UpdateBy = username;
        }

        public virtual void SetDefaultUpdateValue(string username)
        {
            UpdateAt = DateTime.UtcNow;
            UpdateBy = username;
        }
    }
}
