# GroupProjectBackend

## Co jest potrzebne
* SQL Server express 2017
* SQL Server Management Studio
* VisualStudio (ja mam Community 2017)

## SQL Server Express 
Proces instalacji można praktycznie przeklikać. Polecam nazwać serwer "SQLEXPRESS" chociaż taka nazwa ustawiana jest chyba domyślnie. Trzeba też zaznaczyć opcję "Authentication Method" na "Mixed - SQL and windows blabla".
Jak coś pójdzie nie tak, to spokojnie, w SMS da się to raczej naprawić i wyklikać.

## SQL Server Management Studio
Jeśli dobrze udało się postawić Expressa to tutaj powinno wam znajdywać lokalny serwer (coś w stylu "DESKTOP-BLABLALBA\SQLEXPRESS).
Musimy utworzyć bazę "GroupProject" oraz dodać do niej użytkownika "backend" z hasłem "backend".

## VisualStudio
VisualStudio po odpaleniu projektu powinien wszystkie nugety zaciągnąć i w teorii powinno to działać. Jeśli mamy gotową bazę, możemy spróbować się z nią połączyć poprzez "Tools">"Connect to Database"
Odpalamy "Packer Manager Console" ("View">"Other Windows">"Package Manager Console") i wklepujemy "update-database". Jeśli wszystko jest okej to w bazie powinno zostać utworzone parę tabel ("AspNetUsers", "AspNetRoles" itd.)
