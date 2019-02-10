using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Database.Data.Model
{
    public class ParityDependentScheduleSubject : ScheduleField
    {
        internal sealed class ParityDependentConfiguration : IEntityTypeConfiguration<ParityDependentScheduleSubject>
        {
            public void Configure(EntityTypeBuilder<ParityDependentScheduleSubject> builder) => builder.HasBaseType<ScheduleField>();
        }

        protected ParityDependentScheduleSubject() { }

        private SubjectInstance _numeratorSubjectInstance;
        private SubjectInstance _denominatorSubjectInstance;

        public ParityDependentScheduleSubject(SubjectInstance numeratorSubjectInstance, SubjectInstance denominatorSubjectInstance, int order, DayOfWeek weekday, Group group) : base(order, weekday, group)
        {
            _numeratorSubjectInstance = numeratorSubjectInstance ?? throw new ArgumentNullException(nameof(numeratorSubjectInstance));
            _denominatorSubjectInstance = denominatorSubjectInstance ?? throw new ArgumentNullException(nameof(denominatorSubjectInstance));
        }

        public SubjectInstance ParitySubjectInstance { get => _numeratorSubjectInstance; set => _numeratorSubjectInstance = value; }
        public SubjectInstance NotParitySubjectInstance { get => _denominatorSubjectInstance; set => _denominatorSubjectInstance = value; }

        public override SubjectInstance SubjectInstance => Group.Parity ? ParitySubjectInstance : NotParitySubjectInstance;
    }
}
