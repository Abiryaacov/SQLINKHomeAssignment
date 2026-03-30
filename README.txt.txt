* Home Assignment - Full Stack

אפליקציית Angular עם .NET Web API ו-SQL Server.

* מה צריך להתקין לפני הכל

- .NET 8 SDK
- Node.js 21
- SQL Server
- Angular CLI 

* איך מריצים את השרת

נכנסים לתיקיית SQLINKHomeAssignment ומריצים:

    dotnet run

השרת עולה על https://localhost:7066

לפני הריצה הראשונה כדאי לבדוק את appsettings.json ולוודא שה-connection string מתאים לסביבה שלכם. אם אתם על SQLEXPRESS תצטרכו לשנות את Server=localhost ל-Server=.\SQLEXPRESS.

ה-DB נוצר אוטומטית בעליית השרת הראשונה, אין צורך לעשות כלום ידנית.

Swagger זמין ב-https://localhost:7066/swagger

* איך מריצים את הפרונט

נכנסים לתיקיית client:

    npm install
    ng serve

האפליקציה עולה על http://localhost:4200

חשוב שהשרת יהיה פעיל לפני שנכנסים לאפליקציה.

* משתמשים לבדיקה

alice@test.com / Password1

bob@test.com / Password2

# זמן פיתוח

כ-10 שעות
