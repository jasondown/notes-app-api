-- liquibase formatted sql

-- changeset jdown:0001
CREATE TABLE Note (
    id SERIAL PRIMARY KEY,
    title VARCHAR(255),
    content VARCHAR(255)
);

-- rollback drop table Note