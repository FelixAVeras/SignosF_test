-- Returns the 5 expensive products for each category
WITH RankedProducts AS (
    SELECT
        p.Id,
        p.Name,
        p.Price,
        p.CategoryId,
        ROW_NUMBER() OVER (PARTITION BY p.CategoryId ORDER BY p.Price DESC) AS PriceRank
    FROM Product p
)
SELECT
    rp.Id,
    rp.Name,
    rp.Price,
    c.Name AS CategoryName
FROM RankedProducts rp
JOIN Category c ON rp.CategoryId = c.Id
WHERE rp.PriceRank <= 5
ORDER BY c.Name, rp.PriceRank;

-- Group stock by category and count products
SELECT
    c.Id AS CategoryId,
    c.Name AS CategoryName,
    SUM(p.Stock) AS TotalStock
FROM Category c
LEFT JOIN Product p ON p.CategoryId = c.Id
GROUP BY c.Id, c.Name
ORDER BY c.Name;

-- Use a CTE to calculate the average price and return products that exceed it
WITH AvgPrice AS (
    SELECT
        AVG(Price) AS AveragePrice
    FROM Product
)
SELECT
    p.Id,
    p.Name,
    p.Price,
    c.Name AS CategoryName,
    ap.AveragePrice
FROM Product p
CROSS JOIN AvgPrice ap
JOIN Category c ON p.CategoryId = c.Id
WHERE p.Price > ap.AveragePrice
ORDER BY p.Price DESC;