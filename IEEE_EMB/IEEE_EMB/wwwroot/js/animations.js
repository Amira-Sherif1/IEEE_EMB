//document.addEventListener('DOMContentLoaded', () => {
//    // Add smooth scroll for navigation links
//    document.querySelectorAll('a[href^="#"]').forEach(anchor => {
//        anchor.addEventListener('click', function (e) {
//            e.preventDefault();
//            const target = document.querySelector(this.getAttribute('href'));
//            if (target) {
//                target.scrollIntoView({
//                    behavior: 'smooth',
//                    block: 'start'
//                });
//            }
//        });
//    });

//    // Add hover effect for cards
//    const cards = document.querySelectorAll('.card');
//    cards.forEach(card => {
//        card.addEventListener('mousemove', (e) => {
//            const rect = card.getBoundingClientRect();
//            const x = e.clientX - rect.left;
//            const y = e.clientY - rect.top;

//            card.style.setProperty('--mouse-x', `${x}px`);
//            card.style.setProperty('--mouse-y', `${y}px`);
//        });
//    });

//    // Animate elements when they come into view
//    const observerOptions = {
//        threshold: 0.1,
//        rootMargin: '0px'
//    };

//    const observer = new IntersectionObserver((entries) => {
//        entries.forEach(entry => {
//            if (entry.isIntersecting) {
//                entry.target.classList.add('visible');
//                observer.unobserve(entry.target);
//            }
//        });
//    }, observerOptions);

//    document.querySelectorAll('.card, .footer-section').forEach(el => {
//        observer.observe(el);
//    });
//});