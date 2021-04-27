dotnet publish -c Release
docker build -t server .
docker tag server registry.heroku.com/server/web
docker push registry.heroku.com/server/web
heroku container:release web -a server
echo press any key
read end