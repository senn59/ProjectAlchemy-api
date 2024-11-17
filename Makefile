migration:
	dotnet ef migrations add $(NAME) -p ProjectAlchemy.Persistence -s ProjectAlchemy.Web
update:
	dotnet ef database update -p ProjectAlchemy.Persistence -s ProjectAlchemy.Web
prod:
	sudo docker compose up --build -d --remove-orphans api
down:
	sudo docker compose down
test:
	sudo docker compose run --build --rm test
