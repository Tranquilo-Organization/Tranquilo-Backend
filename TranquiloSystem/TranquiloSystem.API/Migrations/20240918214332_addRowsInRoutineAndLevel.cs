using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TranquiloSystem.API.Migrations
{
    /// <inheritdoc />
    public partial class addRowsInRoutineAndLevel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Type",
                table: "Routines",
                type: "nvarchar(100)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.InsertData(
                table: "Levels",
                columns: new[] { "Id", "Description", "MaxScore", "MinScore", "Name" },
                values: new object[,]
                {
                    { 1, "Routines for mild anxiety", 20, 0, "Mild Anxiety" },
                    { 2, "Routines for moderate anxiety", 40, 21, "Moderate Anxiety" },
                    { 3, "Routines for severe anxiety", 60, 41, "Severe Anxiety" }
                });

            migrationBuilder.InsertData(
                table: "Routines",
                columns: new[] { "Id", "Description", "LevelId", "Name", "Steps", "Type" },
                values: new object[,]
                {
                    { 1, "A calming morning routine", 1, "Deep Breathing & Positive Affirmations", "1. Upon waking up, sit in a comfortable position.\n2. Inhale deeply for 4 seconds, hold for 7 seconds, and exhale for 8 seconds (repeat for 5 minutes).\n3. Say aloud 3 positive affirmations (e.g., 'I am calm and in control of my day').\n4. Stretch your arms and body for 2 minutes.\n5. Drink a glass of water.", "Morning" },
                    { 2, "A refreshing day routine", 1, "Short Walk & Gratitude Journal", "1. Take a 10-minute walk during a break (preferably in nature or outside).\n2. Focus on your breathing as you walk, counting each step as a relaxation technique.\n3. After your walk, take 5 minutes to write in your gratitude journal.\n4. List 3 things you are thankful for.", "Day" },
                    { 3, "A relaxing night routine", 1, "Meditation", "1. Before going to bed, find a quiet place.\n2. Sit or lie down comfortably.\n3. Close your eyes and practice a 10-minute guided meditation (can use apps like Calm or Headspace).\n4. Focus on letting go of the day's worries.", "Night" },
                    { 4, "A comprehensive morning routine", 2, "Morning Stretch & Mindful Breathing", "1. Start the day with a 10-minute full-body stretch.\n2. Follow with mindful breathing (inhale for 5 seconds, hold for 5 seconds, exhale for 5 seconds).\n3. Write a to-do list with small, achievable tasks for the day.", "Morning" },
                    { 5, "Midday routine for grounding", 2, "Breathing & Grounding Exercises", "1. During the day, take 5-minute breaks every 2 hours to do grounding exercises.\n2. Practice the 5-4-3-2-1 technique (name 5 things you can see, 4 things you can touch, 3 things you can hear, 2 things you can smell, 1 thing you can taste).\n3. Pair this with slow breathing exercises (inhale for 4 seconds, exhale for 6 seconds).", "Day" },
                    { 6, "A calming night routine", 2, "Journaling & Relaxation", "1. Before bed, write down your thoughts and worries in a journal.\n2. Focus on any recurring thoughts or anxiety triggers.\n3. Practice progressive muscle relaxation for 15 minutes (tense each muscle group for 5 seconds, then relax).\n4. Turn off all electronics 30 minutes before bed.", "Night" },
                    { 7, "A grounding morning routine", 3, "Grounding & Emotional Check-in", "1. Upon waking, do a 5-minute grounding exercise (focus on the present moment, noting your surroundings).\n2. Emotionally check-in with yourself. Identify and label any anxious thoughts.\n3. Practice slow, deep breathing for 10 minutes.", "Morning" },
                    { 8, "A structured day routine", 3, "Structured Activity", "1. Break your day into small, manageable chunks of activity (e.g., work for 30 minutes, take a 5-minute break).\n2. During breaks, practice slow, deep breathing or grounding exercises.\n3. Focus on completing 2-3 key tasks rather than overwhelming yourself with a long list.", "Day" },
                    { 9, "A restful night routine", 3, "Guided Imagery & Sleep Preparation", "1. Before bed, spend 10-15 minutes listening to a guided imagery recording (visualize a calm, safe place).\n2. Write down any lingering thoughts or worries in a journal.\n3. Do 10 minutes of deep breathing or muscle relaxation exercises.\n4. Keep electronics away and set the mood for sleep with dim lights or calming music.", "Night" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Routines",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Routines",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Routines",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Routines",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Routines",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Routines",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Routines",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Routines",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Routines",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Levels",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Levels",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Levels",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.AlterColumn<int>(
                name: "Type",
                table: "Routines",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }
    }
}
