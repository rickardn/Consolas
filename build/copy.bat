SET BUILD=Release

MD ..\NuGet\Consolas\lib\net40
MD ..\NuGet\Consolas.Razor\lib\net45

COPY ..\Source\ConsoleApp.Core\bin\%BUILD%\Consolas.Core.dll ..\NuGet\Consolas\lib\net40
COPY ..\Source\ConsoleApp.Core\bin\%BUILD%\Consolas.Core.xml ..\NuGet\Consolas\lib\net40
COPY ..\Source\Consolas.Razor\bin\%BUILD%\Consolas.Razor.dll ..\NuGet\Consolas.Razor\lib\net45
COPY ..\Source\Consolas.Razor\bin\%BUILD%\Consolas.Razor.xml ..\NuGet\Consolas.Razor\lib\net45
