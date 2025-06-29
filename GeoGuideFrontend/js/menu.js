
import { createClient } from 'https://cdn.jsdelivr.net/npm/@supabase/supabase-js@2/+esm';

const SUPABASE_URL = 'https://zitxawcefyuhfhqnybyg.supabase.co';
const SUPABASE_ANON_KEY = 'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJzdXBhYmFzZSIsInJlZiI6InppdHhhd2NlZnl1aGZocW55YnlnIiwicm9sZSI6ImFub24iLCJpYXQiOjE3NTA3ODg3NzAsImV4cCI6MjA2NjM2NDc3MH0.UCZVX7p38R71IIyR8Nl_HY-w5USCl1wg-QhpCiwLgb8';

const supabase = createClient(SUPABASE_URL, SUPABASE_ANON_KEY);

const authStatusDiv = document.getElementById('authStatus');
const authButtonsDiv = document.getElementById('authButtons');

async function updateAuthUI(session) {
    if (session) {
        authStatusDiv.innerHTML = `Welcome, <strong>${session.user.email}</strong>!`;
        authStatusDiv.className = 'alert alert-success';
        authButtonsDiv.innerHTML = `
            <button class="btn btn-danger" id="logoutButton">
                <i class="bi bi-box-arrow-right"></i> Log Out
            </button>
        `;
        document.getElementById('logoutButton').addEventListener('click', async () => {
            const { error } = await supabase.auth.signOut();
            if (error) {
                console.error('Logout error:', error);
                alert('Logout error occurred.');
            } else {
                window.location.reload();
            }
        });
    } else {
        authStatusDiv.innerHTML = `You are not logged in. <a href="/user/login.html" class="alert-link">Login</a> or <a href="/user/register.html" class="alert-link">register</a> to access all features.`;
        authStatusDiv.className = 'alert alert-info';
        authButtonsDiv.innerHTML = `
            <button class="btn btn-primary" onclick="window.location.href='/user/login.html'">
                <i class="bi bi-box-arrow-in-right"></i> Login
            </button>
            <button class="btn btn-success" onclick="window.location.href='/user/register.html'">
                <i class="bi bi-person-plus"></i> Register
            </button>
        `;
    }
}

async function checkInitialSession() {
    const { data: { session }, error } = await supabase.auth.getSession();
    if (error) {
        console.error('Database error', error);
        authStatusDiv.textContent = 'Authorization error';
        authStatusDiv.className = 'alert alert-danger';
    } else {
        updateAuthUI(session);
    }
}

supabase.auth.onAuthStateChange((event, session) => {
    console.log('Authentication state changed:', event, session);
    updateAuthUI(session);
});

checkInitialSession();

document.querySelector('.map-container').addEventListener('click', function() {
    this.classList.add('hidden'); 
});