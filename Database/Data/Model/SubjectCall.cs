using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Data.Model
{
    public class SubjectCall : IComparable
    {
        internal class Configuration : IEntityTypeConfiguration<SubjectCall>
        {
            public void Configure(EntityTypeBuilder<SubjectCall> builder)
            {
                builder.HasKey(x => x.ID);
                builder.Property(x => x.StartLesson).IsRequired();
                builder.Property(x => x.EndLesson).IsRequired();
                builder.Property(x => x.Order).IsRequired();
            }
        }

        private int _id;
        private TimeSpan _startLesson;
        private TimeSpan _endLesson;
        private int _order;

        public SubjectCall(TimeSpan startLesson, TimeSpan endLesson, int order)
        {
            _startLesson = startLesson;
            _endLesson = endLesson;
            _order = order;
        }

        public int ID { get => _id; set => _id = value; }
        public TimeSpan StartLesson { get => _startLesson; set => _startLesson = value; }
        public TimeSpan EndLesson { get => _endLesson; set => _endLesson = value; }
        public int Order { get => _order; set => _order = value; }

        public int CompareTo(object obj) => _order.CompareTo(((SubjectCall)obj)._order);
    }
}
