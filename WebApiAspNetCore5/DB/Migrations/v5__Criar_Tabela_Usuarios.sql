CREATE TABLE Usuarios (
  id bigint(20) NOT NULL AUTO_INCREMENT PRIMARY KEY,
  user_name varchar(150) UNIQUE NOT NULL,
  password varchar(300) NOT NULL,
  nome varchar(80) NOT NULL,
  refresh_token varchar(500) NOT NULL,
  refresh_token_expire_time datetime NOT NULL
 

)