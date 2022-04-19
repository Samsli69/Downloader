using Projets;
using System.Net;

//Affichage ASCII
Console.ForegroundColor = ConsoleColor.DarkGreen;
var ascii = new ASCII(@"
____   ____.__    .___                 .___                  .__                    .___
\   \ /   /|__| __| _/____  ____     __| _/______  _  ______ |  |   _________     __| _/
 \   Y   / |  |/ __ |/ __ \/  _ \   / __ |/  _ \ \/ \/ /    \|  |  /  _ \__  \   / __ | 
  \     /  |  / /_/ \  ___(  <_> ) / /_/ (  <_> )     /   |  \  |_(  <_> ) __ \_/ /_/ | 
   \___/   |__\____ |\___  >____/  \____ |\____/ \/\_/|___|  /____/\____(____  /\____ | 
                   \/    \/             \/                 \/                \/      \/ 
");
Console.WriteLine(ascii.ToString());
Console.WriteLine("Lien URL ? ");
var url = Console.ReadLine();

var downloadFileUrl = "https://dv9.sibnet.ru/38/42/04/3842045.mp4?st=YfQC7n1pZCzIdQRhALOJ7g&e=1650391000&stor=9&noip=1";
Uri linkUrl = new(downloadFileUrl);

using (var client = new HttpClientDownloadWithProgress(linkUrl))
{
    
    client.ProgressChanged += (totalFileSize, totalBytesDownloaded, progressPercentage) => {
        Console.WriteLine($"{progressPercentage}% ({totalBytesDownloaded}/{totalFileSize})");
    };

    await client.StartDownload();
}









