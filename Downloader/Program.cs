using Downloader;
using Projets;
using System.Net;

//Affichage ASCII
Console.ForegroundColor = ConsoleColor.Green;
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
Uri linkUrl = new(url);

using (var client = new HttpClientDownloadWithProgress(linkUrl))
{
    
    client.ProgressChanged += (totalFileSize, totalBytesDownloaded, progressPercentage) => {
        ProgressBar.WriteProgressBar(progressPercentage, true);
        //ProgressBar.ClearLastLine();
        //Console.WriteLine($"{progressPercentage}% ({totalBytesDownloaded}/{totalFileSize})");
        
    };

    await client.StartDownload();
}









