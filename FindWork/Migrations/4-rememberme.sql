create table if not exists UserToken (
	UseetTokenId uuid primary key,
	UserId int,
	Created timestamp
)