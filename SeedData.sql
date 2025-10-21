-- ============================================
-- Seed Data for Stock Market App
-- ============================================

-- OPTIONAL: Clear existing data (uncomment if you want to start fresh)
-- DELETE FROM Comments;
-- DELETE FROM CompanyStocks;
-- DBCC CHECKIDENT ('Comments', RESEED, 0);
-- DBCC CHECKIDENT ('CompanyStocks', RESEED, 0);

-- ============================================
-- Insert Company Stocks
-- ============================================
INSERT INTO CompanyStocks (CompanyName, Symbol, Purchase, LastDiv, Industry, MarketCap)
VALUES 
    ('Apple Inc.', 'AAPL', 175.50, 0.96, 'Technology', 2800000000000),
    ('Microsoft Corporation', 'MSFT', 380.25, 2.72, 'Technology', 2700000000000),
    ('Tesla Inc.', 'TSLA', 242.80, 0.00, 'Automotive', 770000000000),
    ('Amazon.com Inc.', 'AMZN', 145.30, 0.00, 'E-commerce', 1500000000000),
    ('NVIDIA Corporation', 'NVDA', 485.60, 0.16, 'Technology', 1200000000000),
    ('Alphabet Inc.', 'GOOGL', 138.20, 0.00, 'Technology', 1750000000000),
    ('Meta Platforms Inc.', 'META', 325.45, 0.00, 'Technology', 850000000000),
    ('Berkshire Hathaway', 'BRK.B', 365.80, 0.00, 'Financial Services', 780000000000),
    ('Johnson & Johnson', 'JNJ', 162.30, 4.52, 'Healthcare', 410000000000),
    ('JPMorgan Chase & Co.', 'JPM', 155.20, 4.00, 'Financial Services', 450000000000),
    ('Visa Inc.', 'V', 245.75, 1.80, 'Financial Services', 520000000000),
    ('Procter & Gamble', 'PG', 155.90, 3.65, 'Consumer Goods', 370000000000),
    ('Coca-Cola Company', 'KO', 59.80, 1.84, 'Beverages', 260000000000),
    ('Walmart Inc.', 'WMT', 168.50, 2.28, 'Retail', 480000000000),
    ('Netflix Inc.', 'NFLX', 445.30, 0.00, 'Entertainment', 195000000000);

-- ============================================
-- Insert Comments
-- ============================================

-- Comments for Stock ID 1 (Apple)
INSERT INTO Comments (Title, Content, CreatedOn, CompanyStockId)
VALUES 
    ('Great long-term hold!', 'Apple continues to dominate with strong ecosystem and loyal customer base.', '2024-10-01 09:30:00', 1),
    ('iPhone sales up', 'Latest iPhone models showing strong sales numbers in Q3.', '2024-10-03 14:20:00', 1),
    ('Services revenue growing', 'Apple Services segment hitting record highs.', '2024-10-05 11:15:00', 1);

-- Comments for Stock ID 2 (Microsoft)
INSERT INTO Comments (Title, Content, CreatedOn, CompanyStockId)
VALUES 
    ('Cloud growth impressive', 'Azure is gaining market share rapidly against AWS.', '2024-09-28 10:45:00', 2),
    ('AI integration exciting', 'Microsoft Copilot integration across products is game-changing.', '2024-10-02 16:30:00', 2),
    ('Solid dividend stock', 'Consistent dividend payments make this a great income play.', '2024-10-04 13:00:00', 2);

-- Comments for Stock ID 3 (Tesla)
INSERT INTO Comments (Title, Content, CreatedOn, CompanyStockId)
VALUES 
    ('Cybertruck deliveries starting', 'Finally seeing Cybertruck production ramping up!', '2024-10-01 15:20:00', 3),
    ('Volatility warning', 'Great company but stock price swings wildly. Be prepared!', '2024-10-06 09:00:00', 3);

-- Comments for Stock ID 4 (Amazon)
INSERT INTO Comments (Title, Content, CreatedOn, CompanyStockId)
VALUES 
    ('AWS still dominating', 'Amazon Web Services continues to be the cash cow.', '2024-09-30 11:30:00', 4),
    ('E-commerce competition heating up', 'Facing more competition from Shopify and others.', '2024-10-03 14:45:00', 4);

-- Comments for Stock ID 5 (NVIDIA)
INSERT INTO Comments (Title, Content, CreatedOn, CompanyStockId)
VALUES 
    ('AI chip leader', 'NVIDIA GPUs are essential for AI development. Huge moat!', '2024-10-02 10:00:00', 5),
    ('Valuation concerns', 'Stock is priced to perfection. Any miss could hurt.', '2024-10-05 15:30:00', 5),
    ('Data center demand strong', 'Enterprise demand for AI chips remains incredibly strong.', '2024-10-06 12:00:00', 5);

-- Comments for Stock ID 6 (Google/Alphabet)
INSERT INTO Comments (Title, Content, CreatedOn, CompanyStockId)
VALUES 
    ('Search still king', 'Google search remains the dominant player worldwide.', '2024-09-29 09:15:00', 6),
    ('YouTube growth', 'YouTube revenue continues to climb with new features.', '2024-10-04 11:45:00', 6);

-- Comments for Stock ID 7 (Meta)
INSERT INTO Comments (Title, Content, CreatedOn, CompanyStockId)
VALUES 
    ('Metaverse pivot risky', 'Investing billions in VR/AR - jury still out on ROI.', '2024-10-01 13:30:00', 7),
    ('Ad revenue rebounding', 'After tough 2022, ad business showing strong recovery.', '2024-10-05 10:20:00', 7);

-- Comments for Stock ID 9 (Johnson & Johnson)
INSERT INTO Comments (Title, Content, CreatedOn, CompanyStockId)
VALUES 
    ('Defensive stock pick', 'Healthcare is recession-proof. Great dividend too!', '2024-09-27 14:00:00', 9),
    ('Pharmaceutical pipeline strong', 'New drug approvals looking promising for next year.', '2024-10-02 09:30:00', 9);

-- Comments for Stock ID 10 (JPMorgan)
INSERT INTO Comments (Title, Content, CreatedOn, CompanyStockId)
VALUES 
    ('Banking sector leader', 'Best managed bank in America. Solid leadership.', '2024-09-26 11:00:00', 10),
    ('Interest rate sensitivity', 'Benefits from higher interest rates environment.', '2024-10-01 15:45:00', 10);

-- Comments for Stock ID 13 (Coca-Cola)
INSERT INTO Comments (Title, Content, CreatedOn, CompanyStockId)
VALUES 
    ('Dividend aristocrat!', 'Been paying dividends for decades. Reliable income stream.', '2024-09-25 10:30:00', 13),
    ('Brand power unmatched', 'Coca-Cola brand recognized in every corner of the world.', '2024-10-03 12:15:00', 13);

-- Comments for Stock ID 15 (Netflix)
INSERT INTO Comments (Title, Content, CreatedOn, CompanyStockId)
VALUES 
    ('Password crackdown working', 'Forcing password sharing users to subscribe is boosting revenue!', '2024-10-04 16:00:00', 15),
    ('Content spending high', 'Spending billions on original content. Can they maintain it?', '2024-10-06 11:30:00', 15);

-- ============================================
-- Verify the data was inserted
-- ============================================
SELECT COUNT(*) AS 'Total Stocks' FROM CompanyStocks;
SELECT COUNT(*) AS 'Total Comments' FROM Comments;

-- View all data
SELECT * FROM CompanyStocks ORDER BY Id;
SELECT * FROM Comments ORDER BY CompanyStockId, CreatedOn;

