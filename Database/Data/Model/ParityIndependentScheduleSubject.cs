using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Database.Data.Model
{
    public class ParityIndependentScheduleSubject : ScheduleField
    {
        internal sealed class ParityIndependentConfiguration : IEntityTypeConfiguration<ParityIndependentScheduleSubject>
        {
            public void Configure(EntityTypeBuilder<ParityIndependentScheduleSubject> builder) => builder.HasBaseType<ScheduleField>();
        }

        protected ParityIndependentScheduleSubject() { }

        private SubjectInstance _subject;

        public ParityIndependentScheduleSubject(SubjectInstance subject, int order, DayOfWeek weekday, Group group) : base(order, weekday, group) => _subject = subject ?? throw new ArgumentNullException(nameof(subject));

        public SubjectInstance Subject { get => _subject; set => _subject = value; }
        public override SubjectInstance SubjectInstance => Subject;
    }
}
