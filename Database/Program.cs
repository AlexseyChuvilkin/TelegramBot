using Database.Data;
using Database.Data.Model;
using System;
using System.Collections.Generic;

namespace Database
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            using (DataContext dataContext = new DataContext())
            {
                //User user = new User(399105658, 399105658, "alexseychik (Alexsey Chuvilkin)");
                //dataContext.Users.Add(user);

                //Group group = new Group("ЭВМ.Б-61", new DateTime(2019, 2, 7), user);
                //dataContext.Groups.Add(group);

                //SubjectCall firstLesson = new SubjectCall(new TimeSpan(8, 30, 0), new TimeSpan(10, 05, 0), 0);
                //SubjectCall secondLesson = new SubjectCall(new TimeSpan(10, 20, 0), new TimeSpan(11, 55, 0), 1);
                //SubjectCall thirdLesson = new SubjectCall(new TimeSpan(12, 10, 0), new TimeSpan(13, 45, 0), 2);
                //SubjectCall fourthLesson = new SubjectCall(new TimeSpan(14, 15, 0), new TimeSpan(15, 50, 0), 3);
                //SubjectCall fifthLesson = new SubjectCall(new TimeSpan(16, 05, 0), new TimeSpan(17, 40, 0), 4);
                //SubjectCall sixthLesson = new SubjectCall(new TimeSpan(17, 50, 0), new TimeSpan(19, 25, 0), 5);

                //dataContext.SubjectCalls.AddRange(new SubjectCall[] { firstLesson, secondLesson, thirdLesson, fourthLesson, fifthLesson, sixthLesson });

                //Subject modeling = new Subject("Моделирование");
                //Subject microprocessorSystem = new Subject("Микропроцессорные системы");
                //Subject ibmArchitecture = new Subject("Архитектура ЭВМ");
                //Subject ibm = new Subject("ЭВМ");
                //Subject physicalCulture = new Subject("Физкультура");
                //Subject russian = new Subject("Русский язык и кулльтура речи");
                //Subject economy = new Subject("Экономика");
                //Subject jurisprudence = new Subject("Правоведение");
                //Subject circuitry = new Subject("Схемотехника");
                //Subject foreign = new Subject("ИНО");
                //Subject lifeSafety = new Subject("БЖД");
                //Subject researchWork = new Subject("НИР");

                //dataContext.Subjects.AddRange(new Subject[] { modeling, microprocessorSystem, ibmArchitecture, ibm, physicalCulture, russian, economy, jurisprudence, circuitry, foreign, lifeSafety, researchWork });

                //SubjectInstance modelingLecture = new SubjectInstance(SubjectType.Lecture, "6-102", "Донецков", modeling);
                //SubjectInstance modelingLaboratory = new SubjectInstance(SubjectType.Laboratory, "3-218", "Донецков", modeling);

                //SubjectInstance microprocessorSystemLecture = new SubjectInstance(SubjectType.Lecture, "3-309", "Николаев", microprocessorSystem);
                //SubjectInstance microprocessorSystemExercise = new SubjectInstance(SubjectType.Exercise, "3-309", "Николаев", microprocessorSystem);
                //SubjectInstance microprocessorSystemLaboratory = new SubjectInstance(SubjectType.Laboratory, "3-309", "Николаев", microprocessorSystem);

                //SubjectInstance ibmArchitectureLecture = new SubjectInstance(SubjectType.Lecture, "3-325", "Онуфриева", ibmArchitecture);
                //SubjectInstance ibmArchitectureLaboratory = new SubjectInstance(SubjectType.Laboratory, "3-318", "Онуфриева", ibmArchitecture);

                //SubjectInstance ibmLecture = new SubjectInstance(SubjectType.Lecture, "3-325", "Онуфриева", ibm);
                //SubjectInstance ibmExercise = new SubjectInstance(SubjectType.Exercise, "3-215", "Онуфриева", ibm);
                //SubjectInstance ibmLaboratory = new SubjectInstance(SubjectType.Laboratory, "3-318", "Онуфриева", ibm);

                //SubjectInstance physicalCultureExercise = new SubjectInstance(SubjectType.Exercise, "к.2", "-", physicalCulture);

                //SubjectInstance russianExercise = new SubjectInstance(SubjectType.Exercise, "3-326", "Логинова", russian);

                //SubjectInstance economyLecture = new SubjectInstance(SubjectType.Lecture, "3-313", "Яловенко", economy);
                //SubjectInstance economyExercise = new SubjectInstance(SubjectType.Exercise, "3-313", "Яловенко", economy);

                //SubjectInstance jurisprudenceLecture = new SubjectInstance(SubjectType.Lecture, "3-403", "Шафигуллина", jurisprudence);
                //SubjectInstance jurisprudenceExercise = new SubjectInstance(SubjectType.Exercise, "3-313", "Шафигуллина", jurisprudence);

                //SubjectInstance circuitryCoursework = new SubjectInstance(SubjectType.Coursework, "к.3", "Максимов", circuitry);

                //SubjectInstance foreignExercise = new SubjectInstance(SubjectType.Exercise, "к.1", "Тунанова", foreign);

                //SubjectInstance lifeSafetyLecture = new SubjectInstance(SubjectType.Lecture, "7-309", "Никулина", lifeSafety);
                //SubjectInstance lifeSafetyExercise = new SubjectInstance(SubjectType.Exercise, "7-305", "Фатеева", lifeSafety);
                //SubjectInstance lifeSafetyLaboratory = new SubjectInstance(SubjectType.Laboratory, "7-307", "Фатеева", lifeSafety);

                //SubjectInstance researchWorkScientificResearch = new SubjectInstance(SubjectType.ScientificResearch, "к.3", "-", researchWork);

                //dataContext.SubjectInstances.AddRange(new SubjectInstance[] { modelingLecture, modelingLaboratory, microprocessorSystemLecture, microprocessorSystemExercise, microprocessorSystemLaboratory,
                //    ibmArchitectureLecture, ibmArchitectureLaboratory, ibmLecture, ibmExercise, ibmLaboratory, physicalCultureExercise, russianExercise,  economyLecture, economyExercise,
                //    jurisprudenceLecture, jurisprudenceExercise, circuitryCoursework, foreignExercise,lifeSafetyLecture,lifeSafetyExercise,lifeSafetyLaboratory,researchWorkScientificResearch });


                //ParityIndependentScheduleSubject mondey0 = new ParityIndependentScheduleSubject(modelingLecture, 0, DayOfWeek.Monday, group);
                //ParityDependentScheduleSubject mondey1 = new ParityDependentScheduleSubject(microprocessorSystemExercise, ibmArchitectureLaboratory, 1, DayOfWeek.Monday, group);
                //ParityDependentScheduleSubject mondey2 = new ParityDependentScheduleSubject(ibmLecture, ibmArchitectureLecture, 2, DayOfWeek.Monday, group);
                //ParityIndependentScheduleSubject mondey3 = new ParityIndependentScheduleSubject(physicalCultureExercise, 3, DayOfWeek.Monday, group);

                //ParityDependentScheduleSubject tuesday0 = new ParityDependentScheduleSubject(russianExercise, ibmExercise, 0, DayOfWeek.Tuesday, group);
                //ParityIndependentScheduleSubject tuesday1 = new ParityIndependentScheduleSubject(economyLecture, 1, DayOfWeek.Tuesday, group);
                //ParityIndependentScheduleSubject tuesday2 = new ParityIndependentScheduleSubject(jurisprudenceLecture, 2, DayOfWeek.Tuesday, group);
                //ParityDependentScheduleSubject tuesday3 = new ParityDependentScheduleSubject(economyExercise, jurisprudenceExercise, 3, DayOfWeek.Tuesday, group);

                //ParityIndependentScheduleSubject wednesday0 = new ParityIndependentScheduleSubject(circuitryCoursework, 0, DayOfWeek.Wednesday, group);
                //ParityIndependentScheduleSubject wednesday1 = new ParityIndependentScheduleSubject(foreignExercise, 1, DayOfWeek.Wednesday, group);
                //ParityIndependentScheduleSubject wednesday2 = new ParityIndependentScheduleSubject(ibmLaboratory, 2, DayOfWeek.Wednesday, group);
                //ParityIndependentScheduleSubject wednesday3 = new ParityIndependentScheduleSubject(physicalCultureExercise, 3, DayOfWeek.Wednesday, group);

                //ParityIndependentScheduleSubject friday0 = new ParityIndependentScheduleSubject(microprocessorSystemLaboratory, 0, DayOfWeek.Friday, group);
                //ParityIndependentScheduleSubject friday1 = new ParityIndependentScheduleSubject(lifeSafetyLecture, 1, DayOfWeek.Friday, group);
                //ParityIndependentScheduleSubject friday2 = new ParityIndependentScheduleSubject(modelingLaboratory, 2, DayOfWeek.Friday, group);
                //ParityIndependentScheduleSubject friday3 = new ParityIndependentScheduleSubject(researchWorkScientificResearch, 3, DayOfWeek.Friday, group);

                //ParityIndependentScheduleSubject saturday0 = new ParityIndependentScheduleSubject(microprocessorSystemLecture, 1, DayOfWeek.Saturday, group);
                //ParityDependentScheduleSubject saturday1 = new ParityDependentScheduleSubject(lifeSafetyExercise, lifeSafetyLaboratory, 2, DayOfWeek.Saturday, group);

                //List<ScheduleField> scheduleFields = new List<ScheduleField> { mondey0, mondey1, mondey2, mondey3, tuesday0, tuesday1, tuesday2, tuesday3, wednesday0, wednesday1, wednesday2, wednesday3, friday0, friday1, friday2, friday3, saturday0, saturday1 };
                //group.ScheduleSubjects = scheduleFields;
                //dataContext.ScheduleFields.AddRange(scheduleFields);

                //dataContext.SaveChanges();
            }
            Console.ReadLine();
        }
    }
}
