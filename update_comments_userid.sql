-- Select all current comments
SELECT * FROM [FinancialAppDB].[dbo].[Comments];

-- Update all existing comments to set UserId
UPDATE [FinancialAppDB].[dbo].[Comments]
SET UserId = '9f3b09e4-e8b4-4fa6-93d9-ceb789c135f2'
WHERE UserId IS NULL OR UserId != '9f3b09e4-e8b4-4fa6-93d9-ceb789c135f2';

-- Verify the update
SELECT 
    Id,
    Title,
    UserId,
    COUNT(*) OVER() AS TotalUpdated
FROM Comments
WHERE UserId = '9f3b09e4-e8b4-4fa6-93d9-ceb789c135f2';

