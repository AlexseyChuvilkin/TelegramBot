using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Data.Model
{
    public class Log
    {
        internal class Configuration : IEntityTypeConfiguration<Log>
        {
            public void Configure(EntityTypeBuilder<Log> builder)
            {
                builder.HasKey(x => x.ID);
                builder.Property(x => x.Name).IsRequired();
            }
        }

        private int _id;
        private string _log;

        protected Log() { }
        public Log(string log) => _log = log;

        public int ID { get => _id; set => _id = value; }
        public string Name { get => _log; set => _log = value; }
    }
}
