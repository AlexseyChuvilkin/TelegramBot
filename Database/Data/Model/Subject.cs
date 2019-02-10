using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Collections.Generic;

namespace Database.Data.Model
{
    public class Subject
    {
        internal sealed class Configuration : IEntityTypeConfiguration<Subject>
        {
            public void Configure(EntityTypeBuilder<Subject> builder)
            {
                builder.HasKey(x => x.ID);
                builder.Property(x => x.Name).IsRequired();
                builder.HasMany(x => x.Instances).WithOne(x => x.Subject);
            }
        }
        protected Subject() { }

        private int _id;
        private string _name;

        public Subject(string name) => _name = name;

        public int ID { get => _id; set => _id = value; }
        public string Name { get => _name; set => _name = value; }
        public virtual ICollection<SubjectInstance> Instances { get; set; }
    }
}
