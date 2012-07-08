Create TRIGGER [dbo].[tiSetFootprintPromotionSearchHistory] ON [dbo].PromotionSearchHistory
   WITH 
 EXECUTE AS CALLER  AFTER INSERT 
  
  AS
BEGIN
  	UPDATE   	PromotionSearchHistory
  	SET		CreatedDate = GETDATE()
	FROM		inserted i
	INNER JOIN	PromotionSearchHistory t ON i.Id = t.Id


END