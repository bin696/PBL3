using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace PBL3
{
    internal static class AdminNavigationManager
    {
        private static readonly Dictionary<Type, Form> _cachedForms = new Dictionary<Type, Form>();

        public static void Navigate<T>(Form current) where T : Form, new()
        {
            if (current is null)
            {
                throw new ArgumentNullException(nameof(current));
            }

            if (current is T)
            {
                return;
            }

            CacheForm(current);
            Form target = GetOrCreate<T>();

            if (ReferenceEquals(current, target))
            {
                return;
            }

            current.Hide();

            if (current.WindowState == FormWindowState.Normal)
            {
                target.StartPosition = FormStartPosition.Manual;
                target.Location = current.Location;
            }

            target.WindowState = current.WindowState;
            target.Show();
            target.BringToFront();
        }

        public static void Navigate(Form current, Form target)
        {
            if (current is null)
            {
                throw new ArgumentNullException(nameof(current));
            }

            if (target is null)
            {
                throw new ArgumentNullException(nameof(target));
            }

            if (ReferenceEquals(current, target) || current.GetType() == target.GetType())
            {
                if (!ReferenceEquals(current, target))
                {
                    target.Dispose();
                }
                return;
            }

            CacheForm(current);

            Type targetType = target.GetType();
            if (_cachedForms.TryGetValue(targetType, out Form? existing) && !existing.IsDisposed)
            {
                target.Dispose();
                target = existing;
            }
            else
            {
                target.FormClosed -= HandleFormClosed;
                target.FormClosed += HandleFormClosed;
                _cachedForms[targetType] = target;
            }

            current.Hide();

            if (current.WindowState == FormWindowState.Normal)
            {
                target.StartPosition = FormStartPosition.Manual;
                target.Location = current.Location;
            }

            target.WindowState = current.WindowState;
            target.Show();
            target.BringToFront();
        }

        public static void Logout(Form current)
        {
            if (current is null)
            {
                throw new ArgumentNullException(nameof(current));
            }

            foreach (Form form in _cachedForms.Values.ToList())
            {
                if (form.IsDisposed || ReferenceEquals(form, current))
                {
                    continue;
                }

                form.FormClosed -= HandleFormClosed;
                form.Close();
            }

            _cachedForms.Clear();

            TrangDangNhap trangDangNhap = new TrangDangNhap();
            trangDangNhap.Show();
            current.Close();
        }

        private static Form GetOrCreate<T>() where T : Form, new()
        {
            Type formType = typeof(T);
            if (_cachedForms.TryGetValue(formType, out Form? existing) && !existing.IsDisposed)
            {
                return existing;
            }

            T created = new T();
            created.FormClosed += HandleFormClosed;
            _cachedForms[formType] = created;
            return created;
        }

        private static void CacheForm(Form form)
        {
            Type formType = form.GetType();
            if (_cachedForms.TryGetValue(formType, out Form? existing) && !existing.IsDisposed)
            {
                return;
            }

            form.FormClosed -= HandleFormClosed;
            form.FormClosed += HandleFormClosed;
            _cachedForms[formType] = form;
        }

        private static void HandleFormClosed(object? sender, FormClosedEventArgs e)
        {
            if (sender is not Form form)
            {
                return;
            }

            Type key = form.GetType();
            if (_cachedForms.TryGetValue(key, out Form? cached) && ReferenceEquals(cached, form))
            {
                _cachedForms.Remove(key);
            }
        }
    }
}
