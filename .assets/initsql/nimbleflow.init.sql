CREATE TABLE IF NOT EXISTS category
(
    id            uuid PRIMARY KEY     DEFAULT gen_random_uuid(),
    title         VARCHAR(32) NOT NULL,
    color_theme   INT         NULL,
    category_icon INT         NULL,
    created_at    TIMESTAMP   NOT NULL DEFAULT NOW(),
    deleted_at    TIMESTAMP   NULL
);

CREATE TABLE IF NOT EXISTS product
(
    id          uuid PRIMARY KEY            DEFAULT gen_random_uuid(),
    title       VARCHAR(64) UNIQUE NOT NULL,
    description VARCHAR(512)       NULL,
    price       DECIMAL            NOT NULL,
    image_url   TEXT               NULL,
    is_favorite BOOLEAN            NOT NULL DEFAULT FALSE,
    category_id uuid REFERENCES category (id),
    created_at  TIMESTAMP          NOT NULL DEFAULT NOW(),
    deleted_at  TIMESTAMP          NULL
);

CREATE TABLE IF NOT EXISTS "table"
(
    id            uuid PRIMARY KEY             DEFAULT gen_random_uuid(),
    accountable   VARCHAR(256) UNIQUE NOT NULL,
    is_fully_paid BOOLEAN             NOT NULL DEFAULT FALSE,
    created_at    TIMESTAMP           NOT NULL DEFAULT NOW(),
    deleted_at    TIMESTAMP           NULL
);

CREATE TABLE IF NOT EXISTS "order"
(
    id         uuid PRIMARY KEY   DEFAULT gen_random_uuid(),
    table_id   uuid REFERENCES "table" (id),
    created_at TIMESTAMP NOT NULL DEFAULT NOW(),
    deleted_at TIMESTAMP NULL
);

CREATE TABLE IF NOT EXISTS order_product
(
    order_id       uuid REFERENCES "order" (id),
    product_id     uuid REFERENCES product (id),
    product_amount INT       NOT NULL,
    created_at     TIMESTAMP NOT NULL DEFAULT NOW(),
    deleted_at     TIMESTAMP NULL,
    PRIMARY KEY (order_id, product_id)
);