document.addEventListener("DOMContentLoaded", () => {
    const quantityInput = document.querySelector("[data-transaction-quantity]");
    const unitPriceInput = document.querySelector("[data-transaction-unitprice]");
    const totalAmountInput = document.querySelector("[data-transaction-total]");

    const updateTotal = () => {
        if (!quantityInput || !unitPriceInput || !totalAmountInput) {
            return;
        }

        const quantity = Number(quantityInput.value || 0);
        const unitPrice = Number(unitPriceInput.value || 0);
        totalAmountInput.value = (quantity * unitPrice).toFixed(2);
    };

    quantityInput?.addEventListener("input", updateTotal);
    unitPriceInput?.addEventListener("input", updateTotal);
});
