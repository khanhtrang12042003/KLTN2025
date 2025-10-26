// FAQ Accordion
const faqItems = document.querySelectorAll('.faq-item');
faqItems.forEach(item => {
    const question = item.querySelector('.faq-question');
    question.addEventListener('click', () => {
        faqItems.forEach(other => other !== item && other.classList.remove('active'));
        item.classList.toggle('active');
    });
});

// Search
document.querySelector('.search-bar button')?.addEventListener('click', () => {
    const term = document.querySelector('.search-bar input').value;
    if (term) alert(`Tìm kiếm: ${term}`);
});

// Filter
document.querySelector('.filter-button')?.addEventListener('click', () => {
    alert('Bộ lọc đang được áp dụng!');
});
