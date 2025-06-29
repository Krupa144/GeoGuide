
import { createClient } from 'https://cdn.jsdelivr.net/npm/@supabase/supabase-js@2/+esm';

const SUPABASE_URL = 'https://zitxawcefyuhfhqnybyg.supabase.co';
const SUPABASE_ANON_KEY = 'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJzdXBhYmFzZSIsInJlZiI6InppdHhhd2NlZnl1aGZocW55YnlnIiwicm9sZSI6ImFub24iLCJpYXQiOjE3NTA3ODg3NzAsImV4cCI6MjA2NjM2NDc3MH0.UCZVX7p38R71IIyR8Nl_HY-w5USCl1wg-QhpCiwLgb8';

const supabase = createClient(SUPABASE_URL, SUPABASE_ANON_KEY);

document.getElementById('registerForm').addEventListener('submit', async function(event) {
    event.preventDefault();

    const email = document.getElementById('email').value;
    const password = document.getElementById('password').value;
    const messageDiv = document.getElementById('register-message');
    messageDiv.style.display = 'none';

    const { data, error } = await supabase.auth.signUp({
        email: email,
        password: password,
    });

    if (error) {
        messageDiv.textContent = 'Registration error: ' + error.message;
        messageDiv.style.display = 'block';
        return;
    }

    alert('Registration successful! Please check your email to confirm your account.');
    window.location.href = '../../user/login.html'; 
});