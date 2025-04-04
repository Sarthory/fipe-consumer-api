export const scrollItemIntoView = (elementId: String) => {
  const listItem = document.getElementById(`${elementId}`);
  listItem?.scrollIntoView({ behavior: 'smooth' });
};
