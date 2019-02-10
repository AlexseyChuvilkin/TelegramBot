using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Database.Data.Model
{
    public abstract class ScheduleField
    {
        internal sealed class Configuration : IEntityTypeConfiguration<ScheduleField>
        {
            public void Configure(EntityTypeBuilder<ScheduleField> builder)
            {
                builder.HasKey(x => x.ID);
                builder.Property(x => x.Order).IsRequired().HasColumnType("int");
                builder.Property(x => x.DayOfWeek).IsRequired().HasColumnType("int");
            }
        }

        protected ScheduleField() { }

        private int _id;
        private int _order;
        private DayOfWeek _weekday;
        private Group _group;

        protected ScheduleField(int order, DayOfWeek weekday, Group group)
        {
            _order = order;
            _weekday = weekday;
            _group = group ?? throw new ArgumentNullException(nameof(group));
        }

        public int ID { get => _id; set => _id = value; }
        public int Order { get => _order; set => _order = value; }
        public DayOfWeek DayOfWeek { get => _weekday; set => _weekday = value; }
        public Group Group { get => _group; set => _group = value; }
    }
}
