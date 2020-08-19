DROP DATABASE IF EXISTS DBStructureDesign;
CREATE DATABASE todoapp_db;
USE todoapp_db; 

CREATE TABLE users	
(
	id INTEGER AUTO_INCREMENT PRIMARY KEY NOT NULL,
    username VARCHAR(255) UNIQUE NOT NULL,
	email VARCHAR(255) UNIQUE NOT NULL,
	user_password VARCHAR(255) NOT NULL,
	created_at TIMESTAMP DEFAULT NOW()
);

INSERT INTO users (username,email,user_password)
VALUES ('abdulmufeez','mufeezmubeen1997@gmail.com',SHA1('mufeez'));

CREATE TABLE user_tasks
(
	user_task VARCHAR(255) DEFAULT "NO TASK",
	task_datetime DATETIME,
	user_id INT NOT NULL,
	created_at TIMESTAMP DEFAULT NOW(),
	FOREIGN KEY (user_id) REFERENCES users(id)
);

INSERT INTO user_tasks (user_task,task_datetime,user_id)
VALUES ('May prh likh kr bara bno ga','2022-01-01 12-23-59',1),('Mujhy kuch samjh nhi a rha','2222-12-12 12-12-12',1);

