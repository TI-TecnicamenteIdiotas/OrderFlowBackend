CREATE TABLE IF NOT EXISTS category
(
    id            uuid PRIMARY KEY                  DEFAULT gen_random_uuid(),
    title         VARCHAR(32) UNIQUE       NOT NULL,
    color_theme   INT                      NULL,
    category_icon INT                      NULL,
    created_at    TIMESTAMP WITH TIME ZONE NOT NULL DEFAULT NOW(),
    deleted_at    TIMESTAMP WITH TIME ZONE NULL
);

CREATE TABLE IF NOT EXISTS product
(
    id          uuid PRIMARY KEY                  DEFAULT gen_random_uuid(),
    title       VARCHAR(64) UNIQUE       NOT NULL,
    description VARCHAR(512)             NULL,
    price       DECIMAL                  NOT NULL,
    image_url   TEXT                     NULL,
    is_favorite BOOLEAN                  NOT NULL DEFAULT FALSE,
    category_id uuid                     NOT NULL REFERENCES category (id),
    created_at  TIMESTAMP WITH TIME ZONE NOT NULL DEFAULT NOW(),
    deleted_at  TIMESTAMP WITH TIME ZONE NULL
);

CREATE TABLE IF NOT EXISTS "table"
(
    id            uuid PRIMARY KEY                  DEFAULT gen_random_uuid(),
    accountable   VARCHAR(256)             NOT NULL,
    is_fully_paid BOOLEAN                  NOT NULL DEFAULT FALSE,
    created_at    TIMESTAMP WITH TIME ZONE NOT NULL DEFAULT NOW(),
    deleted_at    TIMESTAMP WITH TIME ZONE NULL
);