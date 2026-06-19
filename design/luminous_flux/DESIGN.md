---
name: Luminous Flux
colors:
  surface: '#fcf9f8'
  surface-dim: '#dcd9d9'
  surface-bright: '#fcf9f8'
  surface-container-lowest: '#ffffff'
  surface-container-low: '#f6f3f2'
  surface-container: '#f0edec'
  surface-container-high: '#ebe7e7'
  surface-container-highest: '#e5e2e1'
  on-surface: '#1c1b1b'
  on-surface-variant: '#424656'
  inverse-surface: '#313030'
  inverse-on-surface: '#f3f0ef'
  outline: '#727687'
  outline-variant: '#c2c6d8'
  surface-tint: '#0054d6'
  primary: '#0050cb'
  on-primary: '#ffffff'
  primary-container: '#0066ff'
  on-primary-container: '#f8f7ff'
  inverse-primary: '#b3c5ff'
  secondary: '#00677f'
  on-secondary: '#ffffff'
  secondary-container: '#00ccf9'
  on-secondary-container: '#005266'
  tertiary: '#a33200'
  on-tertiary: '#ffffff'
  tertiary-container: '#cc4204'
  on-tertiary-container: '#fff6f4'
  error: '#ba1a1a'
  on-error: '#ffffff'
  error-container: '#ffdad6'
  on-error-container: '#93000a'
  primary-fixed: '#dae1ff'
  primary-fixed-dim: '#b3c5ff'
  on-primary-fixed: '#001849'
  on-primary-fixed-variant: '#003fa4'
  secondary-fixed: '#b7eaff'
  secondary-fixed-dim: '#4cd6ff'
  on-secondary-fixed: '#001f28'
  on-secondary-fixed-variant: '#004e60'
  tertiary-fixed: '#ffdbd0'
  tertiary-fixed-dim: '#ffb59d'
  on-tertiary-fixed: '#390c00'
  on-tertiary-fixed-variant: '#832600'
  background: '#fcf9f8'
  on-background: '#1c1b1b'
  surface-variant: '#e5e2e1'
typography:
  display-lg:
    fontFamily: Hanken Grotesk
    fontSize: 32px
    fontWeight: '800'
    lineHeight: 40px
    letterSpacing: -0.02em
  headline-md:
    fontFamily: Hanken Grotesk
    fontSize: 24px
    fontWeight: '700'
    lineHeight: 32px
    letterSpacing: -0.01em
  headline-sm:
    fontFamily: Hanken Grotesk
    fontSize: 20px
    fontWeight: '700'
    lineHeight: 28px
  body-lg:
    fontFamily: Hanken Grotesk
    fontSize: 18px
    fontWeight: '400'
    lineHeight: 28px
  body-md:
    fontFamily: Hanken Grotesk
    fontSize: 16px
    fontWeight: '400'
    lineHeight: 24px
  label-md:
    fontFamily: Hanken Grotesk
    fontSize: 14px
    fontWeight: '600'
    lineHeight: 20px
    letterSpacing: 0.01em
  label-sm:
    fontFamily: Hanken Grotesk
    fontSize: 12px
    fontWeight: '500'
    lineHeight: 16px
    letterSpacing: 0.02em
  display-lg-mobile:
    fontFamily: Hanken Grotesk
    fontSize: 28px
    fontWeight: '800'
    lineHeight: 36px
rounded:
  sm: 0.25rem
  DEFAULT: 0.5rem
  md: 0.75rem
  lg: 1rem
  xl: 1.5rem
  full: 9999px
spacing:
  base: 4px
  margin-mobile: 20px
  gutter: 16px
  stack-sm: 8px
  stack-md: 16px
  stack-lg: 32px
  section-gap: 48px
---

## Brand & Style

The design system is engineered for high-conversion mobile subscription flows, emphasizing trust, clarity, and momentum. The brand personality is "Professional Energetic"—it feels established enough to handle financial transactions but modern enough to feel cutting-edge. 

The aesthetic leverages **Corporate Modernism** with a heavy lean toward **Minimalism**. It prioritizes a high signal-to-noise ratio, using expansive white space to reduce cognitive load during the decision-making process. Visual interest is generated through vibrant color accents and refined typography rather than decorative elements. The emotional response should be one of confidence and ease, ensuring the user feels in control of their subscription journey.

## Colors

The palette is anchored by "Action Blue," a high-chroma primary color designed to draw the eye to interactive elements and primary conversion paths. 

- **Primary:** A vibrant blue used for buttons, active states, and selection markers.
- **Secondary:** A bright cyan used sparingly for badges (e.g., "Best Value") or data visualization.
- **Neutral:** A deep near-black for high-contrast typography, paired with a series of cool-toned grays for secondary information.
- **Background:** Pure white is the standard to maintain the "clean" aesthetic, with very light gray (#F8FAFC) used for subtle section grouping.

## Typography

The design system utilizes **Hanken Grotesk** across all roles to maintain a unified, contemporary feel. It is a highly legible sans-serif that balances geometric precision with humanist warmth.

- **Headlines:** Use heavy weights (700-800) with slightly tightened letter spacing to create a strong visual "anchor" on the page.
- **Body:** Standardized at 16px for optimal readability on mobile devices, adhering to accessibility standards.
- **Hierarchy:** Use weight rather than just size to differentiate information. For example, price points should use `headline-md` or `display-lg` to ensure immediate visibility.
- **Labels:** Uppercase styling should be reserved for very small auxiliary text or overlines to maintain a professional tone.

## Layout & Spacing

This design system employs a **Fluid Grid** model optimized for mobile-first interaction. 

- **Grid:** A 4-column grid for mobile with 20px outer margins to ensure content doesn't feel cramped against device edges.
- **Vertical Rhythm:** A strict 4px/8px baseline grid ensures consistent spacing between text elements and components.
- **Safety Zones:** Adhere to iOS and Android safe area insets, particularly for "sticky" bottom buttons, ensuring they are never obscured by home indicators or notches.
- **Content Grouping:** Use larger gaps (stack-lg) between distinct sections of the subscription page (e.g., between the feature list and the pricing cards) to allow the design to "breathe."

## Elevation & Depth

Visual hierarchy is established through a mix of **Tonal Layering** and **Ambient Shadows**.

- **Surface Tiers:** The main background is Level 0 (White). Plan selection cards sit on Level 1, utilizing a subtle 1px border (#E2E8F0) or a very soft shadow to indicate interactability.
- **Shadows:** Shadows are highly diffused and low-opacity (5-10% alpha). Use a "soft lift" for primary call-to-action buttons to make them appear tactile and pressable.
- **Active State:** When a plan is selected, it should transition from a subtle border to a 2px Primary Blue border, potentially with a slight scale increase (1.02x) to provide haptic-like visual feedback.

## Shapes

The shape language is "Friendly Professional." Surfaces use a medium corner radius that feels modern and approachable without becoming "bubbly" or juvenile.

- **Standard Elements:** Buttons and input fields use a 0.5rem (8px) radius.
- **Containers:** Large cards and pricing tiers use a 1rem (16px) radius to create a distinct frame for content.
- **Selection Indicators:** Small icons or checkboxes within cards should maintain a consistent 4px radius or be fully circular for status indicators.

## Components

### Buttons
- **Primary CTA:** Solid Primary Blue background with White text. Uses a subtle drop shadow. Full-width on mobile for "thumb-friendly" interaction.
- **Secondary:** Ghost style with a Primary Blue border or plain text with an arrow icon for "Restore Purchase" or "Terms."

### Pricing Cards
- **Unselected:** White background, light gray border (1px).
- **Selected:** White background, 2px Primary Blue border. Includes a "Checkmark" icon in the top right.
- **Badges:** Small, pill-shaped tags (e.g., "SAVE 50%") using Secondary Cyan or Success Green background with high-contrast text.

### Feature Lists
- Use consistent iconography (Primary Blue checkmarks) aligned to the left. 
- Use `body-md` for descriptions. Keep text concise to avoid wrapping beyond two lines.

### Inputs & Toggles
- Form fields have a light gray background (#F1F5F9) to distinguish them from the pure white page background.
- Toggle switches use the Primary Blue for the 'on' state.

### Comparison Table
- On mobile, use a "Feature Row" style where the feature name is a header, followed by clear "Yes/No" or "Included" indicators for each plan tier. High contrast between the rows is maintained via subtle zebra-striping or thin dividers.