

```markdown
# YouTube MP3 Downloader MVC (.NET 8 + Docker)

This is an ASP.NET Core MVC application that allows users to download MP3 audio from YouTube videos using **yt-dlp**. The app is fully containerized using Docker and can be easily run with Docker Compose.

---

## 📦 Features

- Input a YouTube video URL to download audio as MP3
- Uses [yt-dlp](https://github.com/yt-dlp/yt-dlp) and **ffmpeg** for audio extraction
- Built with ASP.NET Core MVC (.NET 8)
- Multi-stage Docker build for efficient container size
- Downloads saved and accessible from a mounted `Downloads` folder
- Easy to run and deploy using Docker Compose

---

## 🚀 Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
- [Docker Desktop](https://www.docker.com/products/docker-desktop)
- Internet connection to download `yt-dlp` binary inside the container

---

## 🔧 Project Structure



YoutubeDownloaderMvc/
├── Controllers/
│   └── HomeController.cs
├── Models/
│   └── DownloadModel.cs
├── Views/
│   ├── Home/
│   │   └── Index.cshtml
│   └── Shared/
│       └── \_Layout.cshtml
├── wwwroot/
│   ├── css/
│   ├── js/
│   └── lib/
├── Dockerfile
├── docker-compose.yml
├── Program.cs
├── appsettings.json
└── YoutubeDownloaderMvc.csproj



---

## 🐳 Docker Setup

### Dockerfile

```dockerfile
# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app
COPY . .
RUN dotnet publish -c Release -o out

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/out .
RUN apt-get update && \
    apt-get install -y ffmpeg curl && \
    curl -L https://github.com/yt-dlp/yt-dlp/releases/latest/download/yt-dlp -o /usr/local/bin/yt-dlp && \
    chmod +x /usr/local/bin/yt-dlp

ENTRYPOINT ["dotnet", "YoutubeDownloaderMvc.dll"]
````

### docker-compose.yml

```yaml
version: '3.8'
services:
  youtube-mp3-app:
    build:
      context: .
      dockerfile: Dockerfile
    ports:
      - "5000:80"
    volumes:
      - ./Downloads:/app/Downloads
```

---

## ▶️ Running the App

1. Open a terminal and navigate to your project directory.
2. Run the app using Docker Compose:

```bash
docker-compose up --build
```

3. Open your browser and go to [http://localhost:5000](http://localhost:5000).
4. Enter a valid YouTube video URL and click **Download MP3**.
5. The MP3 file will be saved in the `Downloads` folder on your machine.

---

## 📅 Notes

* Ensure that the YouTube video URL is accessible and supported by `yt-dlp`.
* The app depends on `yt-dlp` and `ffmpeg` installed inside the container.
* `Downloads` folder is shared between container and host to access downloaded MP3 files.

---

## 🚀 Credits

* [yt-dlp](https://github.com/yt-dlp/yt-dlp)
* [.NET](https://dotnet.microsoft.com/)
* [Docker](https://www.docker.com/)

---

Feel free to contribute or raise issues!

---

**Happy downloading! 🎵**

```

If you want, I can also help you generate the actual project files, controllers, views, or any code you need. Just let me know!
```
