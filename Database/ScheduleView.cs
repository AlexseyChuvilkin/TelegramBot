using Database.Data;
using Database.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Database
{
    static public class ScheduleView
    {
        private struct ScheduleViewItem
        {
            public int Order;
            public SubjectInstance SubjectInstance;

            public ScheduleViewItem(int order, SubjectInstance subjectInstance)
            {
                Order = order;
                SubjectInstance = subjectInstance ?? throw new ArgumentNullException(nameof(subjectInstance));
            }
        }
        private const int _weekDayCount = 7;

        static private string GetWeekDayString(DayOfWeek dayOfWeek)
        {
            switch (dayOfWeek)
            {
                case DayOfWeek.Friday:
                    return "Пятница";
                case DayOfWeek.Monday:
                    return "Понедельник";
                case DayOfWeek.Saturday:
                    return "Суббота";
                case DayOfWeek.Sunday:
                    return "Воскресенье";
                case DayOfWeek.Thursday:
                    return "Четверг";
                case DayOfWeek.Tuesday:
                    return "Вторник";
                case DayOfWeek.Wednesday:
                    return "Среда";
                default:
                    return string.Empty;
            }
        }
        static private string GetSubjectTypeString(SubjectType subjectType)
        {
            switch (subjectType)
            {
                case SubjectType.Lecture:
                    return "Лекция";
                case SubjectType.Laboratory:
                    return "Лабораторная";
                case SubjectType.Seminar:
                    return "Семинар";
                case SubjectType.Exercise:
                    return "Упражнение";
                case SubjectType.Coursework:
                    return "Курсовая";
                case SubjectType.ScientificResearch:
                    return "Научная работа";
                default:
                    return string.Empty;
            }
        }

        static public bool GetSchedule(Group group, out string schedule)
        {
            schedule = string.Empty;
            if (group == null || group.ScheduleSubjects == null)
                return false;

            for (int day = (int)DayOfWeek.Monday; day < _weekDayCount; day++)
            {
                List<ScheduleViewItem> scheduleViewItems = GetSchedule(group, (DayOfWeek)day);
                if (scheduleViewItems == null)
                    continue; 
                schedule += "*" + GetWeekDayString((DayOfWeek)day) + "*\n";

                foreach (ScheduleViewItem subject in scheduleViewItems)
                {
                    schedule +=
                    @"`    " + (subject.Order + 1).ToString() + ". " + subject.SubjectInstance.Subject.Name + " " +
                    subject.SubjectInstance.Audience + " " +
                    subject.SubjectInstance.Teacher + " " +
                    GetSubjectTypeString(subject.SubjectInstance.SubjectType) + "`\n";
                }
                schedule += "\n";
            }
            return true;
        }
        static public bool GetSchedule(Group group, DateTime dateTime, List<SubjectCall> subjectCalls, out string schedule)
        {
            schedule = string.Empty;
            if (group == null || group.ScheduleSubjects == null || subjectCalls == null)
                return false;

            schedule = "*" + GetWeekDayString(dateTime.DayOfWeek) + "*\n\n";

            List<ScheduleViewItem> scheduleViewItems = GetSchedule(group, dateTime.DayOfWeek, group.Parity(dateTime));
            if (scheduleViewItems == null)
                return true;

            foreach (ScheduleViewItem subject in scheduleViewItems)
            {
                schedule +=
                "`" + (subject.Order + 1).ToString() + ". " + subject.SubjectInstance.Subject.Name + "`\n" +
                "▸ `" + GetSubjectTypeString(subject.SubjectInstance.SubjectType) + "`\n" +
                "▸ `" + subject.SubjectInstance.Audience + "`\n" +
                "▸ `" + subject.SubjectInstance.Teacher + "`\n" +
                "▸ `" + subjectCalls[subject.Order].StartLesson.ToString(@"hh\:mm") + " - " + subjectCalls[subject.Order].EndLesson.ToString(@"hh\:mm") + "`\n\n";
            }
            return true;
        }

        static private List<ScheduleViewItem> GetSchedule(Group group, DayOfWeek dayOfWeek, bool parity)
        {
            List<ScheduleField> dayScheduleFields = group.ScheduleSubjects.Where(x => x.DayOfWeek == dayOfWeek).ToList();
            if (dayScheduleFields.Count == 0)
                return null;

            List<ScheduleViewItem> scheduleViewItems = new List<ScheduleViewItem>();

            dayScheduleFields.Sort(new Comparison<ScheduleField>((x, y) => x.Order.CompareTo(y.Order)));
            foreach (ScheduleField subject in dayScheduleFields)
            {
                if (subject is ParityDependentScheduleSubject parityDependentSubject)
                {
                    scheduleViewItems.Add(new ScheduleViewItem(parityDependentSubject.Order, parityDependentSubject.GetSubject(parity)));
                    continue;
                }
                if (subject is ParityIndependentScheduleSubject parityIndependentSubject)
                {
                    scheduleViewItems.Add(new ScheduleViewItem(parityIndependentSubject.Order, parityIndependentSubject.Subject));
                    continue;
                }
            }
            return scheduleViewItems;
        }
        static private List<ScheduleViewItem> GetSchedule(Group group, DayOfWeek dayOfWeek)
        {
            List<ScheduleField> dayScheduleFields = group.ScheduleSubjects.Where(x => x.DayOfWeek == dayOfWeek).ToList();
            if (dayScheduleFields.Count == 0)
                return null;

            List<ScheduleViewItem> scheduleViewItems = new List<ScheduleViewItem>();

            dayScheduleFields.Sort(new Comparison<ScheduleField>((x, y) => x.Order.CompareTo(y.Order)));
            foreach (ScheduleField subject in dayScheduleFields)
            {
                if (subject is ParityDependentScheduleSubject parityDependentSubject)
                {
                    scheduleViewItems.Add(new ScheduleViewItem(parityDependentSubject.Order, parityDependentSubject.ParitySubjectInstance));
                    scheduleViewItems.Add(new ScheduleViewItem(parityDependentSubject.Order, parityDependentSubject.NotParitySubjectInstance));
                    continue;
                }
                if (subject is ParityIndependentScheduleSubject parityIndependentSubject)
                {
                    scheduleViewItems.Add(new ScheduleViewItem(parityIndependentSubject.Order, parityIndependentSubject.Subject));
                    continue;
                }
            }
            return scheduleViewItems;
        }
    }
}
