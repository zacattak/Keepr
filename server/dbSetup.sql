CREATE TABLE IF NOT EXISTS accounts (
    id VARCHAR(255) NOT NULL primary key COMMENT 'primary key', createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created', updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update', name varchar(255) COMMENT 'User Name', email varchar(255) COMMENT 'User Email', picture varchar(255) COMMENT 'User Picture', coverImg VARCHAR(1000)
) default charset utf8mb4 COMMENT '';

CREATE TABLE keeps (
    id INT PRIMARY KEY AUTO_INCREMENT NOT NULL, createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created', updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update', creatorId VARCHAR(255) NOT NULL, name VARCHAR(255) NOT NULL, description VARCHAR(1000) NOT NULL, img VARCHAR(1000) NOT NULL, views INT NOT NULL DEFAULT 0, FOREIGN KEY (creatorId) REFERENCES accounts (id) ON DELETE CASCADE
)

CREATE TABLE vaults (
    id INT PRIMARY KEY AUTO_INCREMENT NOT NULL, createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created', updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update', creatorId VARCHAR(255) NOT NULL, name VARCHAR(255) NOT NULL, description VARCHAR(1000) NOT NULL, img VARCHAR(1000) NOT NULL, isPrivate BOOLEAN NOT NULL DEFAULT false, FOREIGN KEY (creatorId) REFERENCES accounts (id) ON DELETE CASCADE
)

create TABLE vaultKeeps (
    id INT PRIMARY KEY AUTO_INCREMENT NOT NULL, createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created', updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update', creatorId VARCHAR(255) NOT NULL, vaultId INT NOT NULL, keepId INT NOT NULL, FOREIGN KEY (creatorId) REFERENCES accounts (id) ON DELETE CASCADE, FOREIGN KEY (vaultId) REFERENCES vaults (id) ON DELETE CASCADE, FOREIGN KEY (keepId) REFERENCES keeps (id) ON DELETE CASCADE
)

SELECT keep.*, account.*
FROM keeps keep
    JOIN accounts account ON keep.creatorId = account.id;